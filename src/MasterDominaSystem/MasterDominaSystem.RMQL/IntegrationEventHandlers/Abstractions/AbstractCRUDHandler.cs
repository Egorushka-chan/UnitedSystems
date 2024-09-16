using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using UnitedSystems.CommonLibrary.Messages;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.EventBus;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers.Abstractions
{
    internal abstract class AbstractCRUDHandler<TEntity, TEvent>(
        ILogger logger,
        IServiceProvider serviceProvider
        ) : IIntegrationEventHandler<TEvent>
        where TEvent : IntegrationEvent
        where TEntity : IEntityDB
    {
        protected readonly ILogger logger = logger;
        private readonly IServiceProvider services = serviceProvider;
        protected readonly string TEntityName = typeof(TEntity).Name;

        public async Task Handle(TEvent @event)
        {
            ArgumentNullException.ThrowIfNull(@event, nameof(@event));

            logger.LogTrace("Начало выполнения метода Handle {ThisName}<{EntityName}>" +
            "Событие: {id} {timestamp}", ThisName, TEntityName, @event.ID, @event.TimeStamp);

            var denormalizer = (IEntityDenormalizer<TEntity>?)services.GetKeyedService<IEntityDenormalizer>(typeof(TEntity).GetKey());
            if (denormalizer != null) {
                logger.LogTrace("Получен денормалайзер для {name}", TEntityName);

                string unitedScript = GenerateScript(@event, denormalizer);
                logger.LogTrace("Получен скрипт:\n" +
                    "{script}", unitedScript);

                using (var scope = services.CreateScope()) {
                    MasterContext context = scope.ServiceProvider.GetRequiredService<MasterContext>();
                    await context.Database.ExecuteSqlRawAsync(unitedScript);
                };
                logger.LogTrace("Скрипт исполнен");
            }
        }
        protected abstract string GenerateScript(TEvent @event, IEntityDenormalizer<TEntity> denormalizer);
        protected abstract string ThisName { get; }
    }
}
