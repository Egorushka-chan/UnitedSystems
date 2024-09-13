﻿using System.Security.Cryptography;
using System.Text;

using ManyEntitiesSender.BLL.Models;
using ManyEntitiesSender.BLL.Settings;
using ManyEntitiesSender.RAL.Abstractions;

using Microsoft.Extensions.Options;

using StackExchange.Redis;

using UnitedSystems.CommonLibrary.ManyEntitiesSender;
using UnitedSystems.CommonLibrary.ManyEntitiesSender.Interfaces;

namespace ManyEntitiesSender.RAL.Implementations
{
    public class RedisProvider(IOptions<RedisSettings> settings, IOptions<PackageSettings> packageOptions) : IRedisProvider
    {
        protected RedisSettings Settings { get; set; } = settings.Value;
        protected PackageSettings PackageSettings { get; set; } = packageOptions.Value;

        private static IConnectionMultiplexer __connectionMultiplexer;

        /// <summary>
        /// Возвращает объект из singleton, или создаёт новый, если объект ещё не создан
        /// </summary>
        /// <returns></returns>
        public IConnectionMultiplexer GetConnectionMultiplexer()
        {
            __connectionMultiplexer ??= ConnectionMultiplexer.Connect(Settings.Configuration + ",protocol=resp3");
            return __connectionMultiplexer;
        }

        protected IDatabase GetDatabase()
        {
            return GetConnectionMultiplexer().GetDatabase(Settings.DatabaseID);
        }

        private static byte[] GetMD5(string value)
        {
            byte[] mightinessBytes = Encoding.Default.GetBytes(value);
            byte[] mightinessHashBytes = MD5.HashData(mightinessBytes);
            return mightinessHashBytes;
        }
        private Task<long> IncreaseCounter(string table, string value)
        {
            return GetDatabase().StringIncrementAsync(new RedisKey($"counter:{table}:{value}"), 1);
        }

        private async Task<long> GetCounter(string table, string value)
        {
            RedisValue counter = await GetDatabase().StringGetAsync(new RedisKey($"counter:{table}:{value}"));
            if (counter.IsNullOrEmpty)
                return 0;

            counter.TryParse(out long result);
            return result;
        }

        private async Task<List<string>> GetUniqueValues<TEntity>() where TEntity: class, IEntity
        {
            RedisValue[] values = await GetDatabase().SetMembersAsync(new RedisKey($"{typeof(TEntity).Name}:valuesset"));
            return (from value in values
                   select value.ToString()).ToList();
        }

        private Task<bool> SetUniqueValue(string table, string value)
        {
            return GetDatabase().SetAddAsync($"{table}:valuesset", new RedisValue($"{value}"));
        }

        /// <inheritdoc/>
        public async Task AppendListAsync<TEntity>(TEntity[] array) where TEntity: class, IEntity
        {
            // Проверка что тут реализован тип, который будет использоваться
            Type type = typeof(TEntity);
            Type[] allowedTypes = [typeof(Body), typeof(Hand), typeof(Leg)];
            if (!allowedTypes.Contains(type))
                throw new ArgumentException($"Type {type.Name} can't be inserted inside Redis (Not Implemented)");

            // Зачем тут этот делегат? Чтобы не просчитывать выбор типа каждый раз в блоке foreach далее
            Func<List<HashEntry>, IEntity, string> appendFields;
            appendFields = SelectFieldAppendingAlgorithm(type);

            // Получаем количество элементов для того, чтобы в возвращаемой задаче реализовать ожидание
            int entityCount = array.Length;
            long res = 0;
            foreach (TEntity element in array)
            {
                List<HashEntry> fields = [];
                HashEntry idEntry = new(new RedisValue("id"), new RedisValue(element.ID.ToString()));
                fields.Add(idEntry);

                string value = appendFields.Invoke(fields, element);

                long counter = await GetCounter($"{nameof(Body)}", $"{value}");

#pragma warning disable CS4014 // Так как этот вызов не ожидается, выполнение существующего метода продолжается до тех пор, пока вызов не будет завершен

                // Добавляем в список уникальных значений
                SetUniqueValue(type.Name, value);
                RedisKey key = new($"{type.Name}:{value}:{counter + 1}");
                HashEntry[] hashEntries = [.. fields];

                // Все запросы делаются в пайплайне, и не ожидаются. Ожидаться они будут задачей далее.
                Task setter = GetDatabase().HashSetAsync(key, hashEntries);
                setter.ContinueWith((antecedent) =>
                    {
                        entityCount--;
                    });
#pragma warning restore CS4014 // Так как этот вызов не ожидается, выполнение существующего метода продолжается до тех пор, пока вызов не будет завершен

                res = await IncreaseCounter($"{type.Name}", $"{value}");
            }

            // Эта задача нужна для возврата того, что можно ожидать
            Task awaitedTask = Task.Run(() =>
            {
                bool succeeded = false;
                while (!succeeded)
                {
                    if (entityCount >= 0)
                    {
                        succeeded = true;
                    }
                }
            });
            await awaitedTask;
            return;
        }

        /// <inheritdoc/>
        public async IAsyncEnumerable<List<TEntity>> TryGetAsync<TEntity>(string? filterValue = null) where TEntity : class, IEntity, new()
        {
            Type type = typeof(TEntity);
            Type[] allowedTypes = [typeof(Body), typeof(Hand), typeof(Leg)];
            if (!allowedTypes.Contains(type))
                throw new ArgumentException($"Type {type.Name} can't be gotten from Redis (Not Implemented)");

            if (filterValue != null)
            {
                // Входим сюда, если запрошено конкретное value
                long count = await GetCounter(type.Name, filterValue);
                if (count <= 0)
                    yield break;

                int packageCount = PackageSettings.PackageCount;
                int initialIteration = 0;
                int initialElement = 1;
                long requiredIteration = count / packageCount;
                if (count % packageCount != 0)
                    requiredIteration++;

                Func<HashEntry[], IEntity> assertValues;
                assertValues = SelectFieldAssertionAlgorithm(type);

                for (int iteration = initialIteration; iteration < requiredIteration; iteration++)
                {
                    List<TEntity> packageList = [];

                    for (int i = initialElement; i <= packageCount; i++)
                    {
                        int element = i + (iteration * packageCount);
                        if (element > count)
                            break;

                        HashEntry[] values = await GetDatabase().HashGetAllAsync(new RedisKey($"{type.Name}:{filterValue}:{element}"));

                        TEntity entity = assertValues(values) as TEntity ?? throw new InvalidOperationException("Непонятная ошибка преобразования");
                        packageList.Add(entity);
                    }

                    yield return packageList;
                }
            }
            else {
                // Входим сюда, если запрошены все значения
                // Код повторяется, 
                // потому что как провести IAsyncEnumerable через несколько методов адекватно я понятия не имею
                List<string> uniqueValues = await GetUniqueValues<TEntity>();
                foreach(string uniqueValue in uniqueValues)
                {
                    long count = await GetCounter(type.Name, uniqueValue);
                    int packageCount = PackageSettings.PackageCount;
                    long requiredIterations = count/packageCount;
                    if (count % packageCount != 0)
                        requiredIterations++;

                    Func<HashEntry[], IEntity> assertValues;
                    assertValues = SelectFieldAssertionAlgorithm(type);
                
                    for(int iteration = 0; iteration < requiredIterations; iteration++)
                    {
                        List<TEntity> packageList = [];

                        for(int i = 1; i <= packageCount; i++)
                        {
                            int element = i + (iteration * packageCount);
                            if (element > count)
                                break;

                            HashEntry[] values = await GetDatabase().HashGetAllAsync(new RedisKey($"{type.Name}:{uniqueValue}:{element}"));

                            TEntity entity = assertValues(values) as TEntity ?? throw new InvalidOperationException("Непонятная ошибка преобразования");
                            packageList.Add(entity);
                        }

                        yield return packageList;
                    }
                }
            }
        }

        /// <summary>
        /// Для того чтобы не делать каждый раз проверки и выбор алгоритма для каждого типа в <see cref="AppendListAsync{TEntity}(List{TEntity})"/>,
        /// он делается один раз заранее здесь
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private static Func<List<HashEntry>, IEntity, string> SelectFieldAppendingAlgorithm(Type type)
        {
            Func<List<HashEntry>, IEntity, string> appendFields;
            if (type == typeof(Body))
            {
                appendFields = (List<HashEntry> fields, IEntity entity) =>
                {
                    HashEntry mightiness = new(new RedisValue("mightiness"), new RedisValue(((Body)entity).Mightiness));

                    fields.Add(mightiness);

                    return ((Body)entity).Mightiness;
                };
            }
            else if (type == typeof(Hand))
            {
                appendFields = (List<HashEntry> fields, IEntity entity) =>
                {
                    HashEntry state = new(new RedisValue("state"), new RedisValue(((Hand)entity).State));

                    fields.Add(state);

                    return ((Hand)entity).State;
                };
            }
            else if (type == typeof(Leg))
            {
                appendFields = (List<HashEntry> fields, IEntity entity) =>
                {
                    HashEntry state = new(new RedisValue("state"), new RedisValue(((Leg)entity).State));

                    fields.Add(state);

                    return ((Leg)entity).State;
                };
            }
            else
            {
                throw new ArgumentException($"Type {type.Name} can't be inserted inside Redis (Not Implemented)");
            }

            return appendFields;
        }

        /// <summary>
        /// Для того чтобы не делать каждый раз проверки и выбор алгоритма для каждого типа в <see cref="TryGetAsync{TEntity}(EntityFilterOptions)"/>,
        /// он делается один раз заранее здесь
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private static Func<HashEntry[], IEntity> SelectFieldAssertionAlgorithm(Type type)
        {
            // Не ошибка, а дебилизм. entry.Value анализатор принимает за nullable тип, хотя это value-тип. Т.е по факту ошибки нет
            #pragma warning disable CS8604, CS8601 // Возможно, аргумент-ссылка, допускающий значение NULL
            return (HashEntry[] values) =>
            {
                if (type == typeof(Body))
                {
                    Body body = new();
                    foreach (HashEntry entry in values)
                    {
                        if (entry.Name == "id")
                            body.ID = int.Parse(entry.Value);
                        else if (entry.Name == "mightiness")
                            body.Mightiness = entry.Value;
                    }
                    return body;
                }
                else if (type == typeof(Hand))
                {
                    Hand hand = new();
                    foreach (HashEntry entry in values)
                    {
                        if (entry.Name == "id")
                            hand.ID = int.Parse(entry.Value);
                        else if (entry.Name == "state")
                            hand.State = entry.Value;
                    }
                    return hand;
                }
                else if (type == typeof(Leg))
                {
                    Leg leg = new();
                    foreach (HashEntry entry in values)
                    {
                        if (entry.Name == "id")
                            leg.ID = int.Parse(entry.Value);
                        else if (entry.Name == "state")

                            leg.State = entry.Value;
                    }
                    return leg;
                }
                else
                    throw new ArgumentException($"Type {type.Name} can't be gotten from Redis (Not Implemented)");
            };
            #pragma warning restore CS8604, CS8601 // Возможно, аргумент-ссылка, допускающий значение NULL.
        }
    }
}
