using System.Reflection.Metadata;

using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Implementations;
using MasterDominaSystem.BLL.Services.Strategies;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace MasterDominaSystem.BLL
{
    public static class InjectorBLL
    {
        public static IDenormalizerBuilder InjectBLL(this IServiceCollection services)
        {
            services.AddSingleton<ISessionInfoProvider, SessionInfoProvider>();

            services.AddSingleton<IReportsCollector, ReportsCollector>();
            services.AddSingleton<IProcedureBaker, ProcedureBaker>();

            return new DenormalizerBuilder(services);
        }

        /// <summary>
        /// Подключает общие стратегии денормализации. Должна идти после AddDenormalizationStrategy
        /// </summary>
        /// <remarks>
        /// Если какая-то стратегия была явно указана через <see cref="AddDenormalizationStrategy"/>, 
        /// то он её заново не добавляет.
        /// </remarks>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IDenormalizerBuilder AddDefaultDenormalizationStrategies(this IDenormalizerBuilder builder)
        {
            if (!builder.GeneralStrategiesCreated) {
                builder.Services.TryAddKeyedTransient<IEntityDenormalizer, PersonDenormalizer>(typeof(Person).GetKey());
                builder.Services.TryAddKeyedTransient<IEntityDenormalizer, PhysiqueDenormalizer>(typeof(Physique).GetKey());
                builder.Services.TryAddKeyedTransient<IEntityDenormalizer, SetDenormalizer>(typeof(Set).GetKey());
                builder.Services.TryAddKeyedTransient<IEntityDenormalizer, SeasonDenormalizer>(typeof(Season).GetKey());
                builder.Services.TryAddKeyedTransient<IEntityDenormalizer, ClothDenormalizer>(typeof(Cloth).GetKey());
                builder.Services.TryAddKeyedTransient<IEntityDenormalizer, SetHasClothesDenormalizer>(typeof(SetHasClothes).GetKey());
                builder.Services.TryAddKeyedTransient<IEntityDenormalizer, PhotoDenormalizer>(typeof(Photo).GetKey());
                builder.Services.TryAddKeyedTransient<IEntityDenormalizer, MaterialDenormalizer>(typeof(Material).GetKey());
                builder.Services.TryAddKeyedTransient<IEntityDenormalizer, ClothHasMaterialsDenormalizer>(typeof(ClothHasMaterials).GetKey());
            }
            else
                throw new InvalidOperationException("Попытка вызвать метод 2 раза");

            return builder;
        }

        /// <summary>
        /// Позволяет специфически настроить стратегию денормализации
        /// </summary>
        /// <typeparam name="TEntityDB">Объект базы данных</typeparam>
        /// <param name="builder">Получить объект можно через <see cref="InjectBLL"/></param>
        /// <param name="options">Лямбда для задания опций денормализации</param>
        public static IDenormalizerBuilder AddDenormalizationStrategy<TEntityDB>(this IDenormalizerBuilder builder, Action<DenormalizationOptions>? options = default)
            where TEntityDB : IEntityDB, new()
        {

            if (builder.GeneralStrategiesCreated)
                throw new InvalidOperationException("Нельзя настроить стратегии после вызова AddDefaultDenormalizationStrategies");
            // Миллионы switch-case'ов вышли потому что я хочу в методе понятным языком обозначить, для кого денормализацию настраиваем, через всем понятные объекты
            // И сохранить стратегии internal
            TEntityDB entity = new();
            switch (entity) {
                case Person:
                    builder.Services.AddKeyedTransient<IEntityDenormalizer, PersonDenormalizer>(typeof(TEntityDB).GetKey(), (ser, obj) => {
                        IWebHostEnvironment env = ser.GetRequiredService<IWebHostEnvironment>();
                        IReportsCollector collector = ser.GetRequiredService<IReportsCollector>();
                        IProcedureBaker baker = ser.GetRequiredService<IProcedureBaker>();
                        return new PersonDenormalizer(env, collector, baker, options);
                    });
                    break;
                case Cloth:
                    builder.Services.AddKeyedTransient<IEntityDenormalizer, ClothDenormalizer>(typeof(TEntityDB).GetKey(), (ser, obj) => {
                        IWebHostEnvironment env = ser.GetRequiredService<IWebHostEnvironment>();
                        IReportsCollector collector = ser.GetRequiredService<IReportsCollector>();
                        IProcedureBaker baker = ser.GetRequiredService<IProcedureBaker>();
                        return new ClothDenormalizer(env, collector, baker, options);
                    });
                    break;
                case ClothHasMaterials:
                    builder.Services.AddKeyedTransient<IEntityDenormalizer, ClothHasMaterialsDenormalizer>(typeof(TEntityDB).GetKey(), (ser, obj) => {
                        IWebHostEnvironment env = ser.GetRequiredService<IWebHostEnvironment>();
                        IReportsCollector collector = ser.GetRequiredService<IReportsCollector>();
                        IProcedureBaker baker = ser.GetRequiredService<IProcedureBaker>();
                        return new ClothHasMaterialsDenormalizer(env);
                    });
                    break;
                case Material:
                    builder.Services.AddKeyedTransient<IEntityDenormalizer, MaterialDenormalizer>(typeof(TEntityDB).GetKey(), (ser, obj) => {
                        IWebHostEnvironment env = ser.GetRequiredService<IWebHostEnvironment>();
                        IReportsCollector collector = ser.GetRequiredService<IReportsCollector>();
                        IProcedureBaker baker = ser.GetRequiredService<IProcedureBaker>();
                        return new MaterialDenormalizer(env, collector, baker, options);
                    });
                    break;
                case Photo:
                    builder.Services.AddKeyedTransient<IEntityDenormalizer, PhotoDenormalizer>(typeof(TEntityDB).GetKey(), (ser, obj) => {
                        IWebHostEnvironment env = ser.GetRequiredService<IWebHostEnvironment>();
                        IReportsCollector collector = ser.GetRequiredService<IReportsCollector>();
                        IProcedureBaker baker = ser.GetRequiredService<IProcedureBaker>();
                        return new PhotoDenormalizer(env, collector, baker, options);
                    });
                    break;
                case Physique:
                    builder.Services.AddKeyedTransient<IEntityDenormalizer, PhysiqueDenormalizer>(typeof(TEntityDB).GetKey(), (ser, obj) => {
                        IWebHostEnvironment env = ser.GetRequiredService<IWebHostEnvironment>();
                        IReportsCollector collector = ser.GetRequiredService<IReportsCollector>();
                        IProcedureBaker baker = ser.GetRequiredService<IProcedureBaker>();
                        return new PhysiqueDenormalizer(env, collector, baker, options);
                    });
                    break;
                case Season:
                    builder.Services.AddKeyedTransient<IEntityDenormalizer, SeasonDenormalizer>(typeof(TEntityDB).GetKey(), (ser, obj) => {
                        IWebHostEnvironment env = ser.GetRequiredService<IWebHostEnvironment>();
                        IReportsCollector collector = ser.GetRequiredService<IReportsCollector>();
                        IProcedureBaker baker = ser.GetRequiredService<IProcedureBaker>();
                        return new SeasonDenormalizer(env, collector, baker, options);
                    });
                    break;
                case Set:
                    builder.Services.AddKeyedTransient<IEntityDenormalizer, SetDenormalizer>(typeof(TEntityDB).GetKey(), (ser, obj) => {
                        IWebHostEnvironment env = ser.GetRequiredService<IWebHostEnvironment>();
                        IReportsCollector collector = ser.GetRequiredService<IReportsCollector>();
                        IProcedureBaker baker = ser.GetRequiredService<IProcedureBaker>();
                        return new SetDenormalizer(env, collector, baker, options);
                    });
                    break;
                case SetHasClothes:
                    builder.Services.AddKeyedTransient<IEntityDenormalizer, SetHasClothesDenormalizer>(typeof(TEntityDB).GetKey(), (ser, obj) => {
                        IWebHostEnvironment env = ser.GetRequiredService<IWebHostEnvironment>();
                        IReportsCollector collector = ser.GetRequiredService<IReportsCollector>();
                        IProcedureBaker baker = ser.GetRequiredService<IProcedureBaker>();
                        return new SetHasClothesDenormalizer(env);
                    });
                    break;
                default:
                    throw new NotImplementedException($"Тип {typeof(TEntityDB)} не может быть обработан в стратегии денормализации");
            }

            return builder;
        }
    }
}
