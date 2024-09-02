using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    public class WOCreatedHandler<TEntity>(
        ILogger<WOCreatedCRUDEvent<TEntity>> logger,
        IServiceProvider services
        ) : IIntegrationEventHandler<WOCreatedCRUDEvent<TEntity>>
        where TEntity : IEntityDB
    {
        private readonly ILogger<WOCreatedCRUDEvent<TEntity>> logger = logger;
        private readonly IServiceProvider services = services;
        private readonly string TEntityName = typeof(TEntity).Name;

        public async Task Handle(WOCreatedCRUDEvent<TEntity> @event)
        {
            if (@event.Entities.Length == 0)
                throw new ArgumentException("Количество элементов на добавление равно нулю", nameof(@event));

            logger.LogTrace("Начало выполнения метода Handle обработчика событий WOCreatedCRUDEvent<{TEntity}>" +
            "Событие: {id} {timestamp}", TEntityName, @event.ID, @event.TimeStamp);

            var denormalizer = (IEntityDenormalizer<TEntity>?)services.GetKeyedService<IEntityDenormalizer>(typeof(TEntity).GetKey());
            if(denormalizer != null) {
                logger.LogTrace("Получен денормалайзер для {name}", TEntityName);

                string script = "";
                foreach(var entity in @event.Entities) {
                    script += denormalizer.Append(entity);
                }
                logger.LogTrace("Получен скрипт:\n" +
                    "{script}", script);

                using(var scope = services.CreateScope()) {
                    MasterContext context = scope.ServiceProvider.GetRequiredService<MasterContext>();
                    await context.Database.ExecuteSqlRawAsync(script);
                };
                logger.LogTrace("Скрипт исполнен");
            }
        }
    }
}
