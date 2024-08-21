using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace MasterDominaSystem.BLL.Services.Implementations
{
    [Obsolete("Вначале у меня не было нормальных интеграционных событий")]
    public class DatabaseDenormalizer(IServiceProvider services, ILogger<DatabaseDenormalizer> logger,
        MasterContext masterContext) : IDatabaseDenormalizer
    {
        public async Task Append<TEntity>(TEntity entity)
            where TEntity : IEntityDB
        {
            string serviceKey = typeof(TEntity).GetKey();
            logger.LogTrace("Запрошена денормализация для {key}.ID:{id}", serviceKey, entity.ID);

            var entityDenormalizer = services.GetRequiredKeyedService<IEntityDenormalizer>(serviceKey);
            logger.LogTrace("Найден сервис для {serviceKey}", serviceKey);

            await AtomicAppend(entity, entityDenormalizer);
        }

        public async Task AppendRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : IEntityDB
        {
            if (!entities.Any()) {
                throw new ArgumentException("Нет входных элементов, коллекция пустая", nameof(entities));
            }

            string serviceKey = typeof(TEntity).GetKey();
            logger.LogTrace("Запрошена денормализация для {key}'ов", serviceKey);

            var entityDenormalizer = services.GetRequiredKeyedService<IEntityDenormalizer>(serviceKey);
            logger.LogTrace("Найден сервис для {serviceKey}", serviceKey);

            foreach (var entity in entities) {
                await AtomicAppend(entity, entityDenormalizer);
            }
        }

        private async Task AtomicAppend<TEntity>(TEntity entity, IEntityDenormalizer denormalizer)
            where TEntity : IEntityDB
        {
            string query = await denormalizer.Append(entity);

            logger.LogTrace("Выполнение скрипта денормализации:\n{query}", query);
            await masterContext.Database.ExecuteSqlRawAsync(query);
            logger.LogDebug("Выполнен скрипт денормализации:\n{query}", query);
        }
    }
}
