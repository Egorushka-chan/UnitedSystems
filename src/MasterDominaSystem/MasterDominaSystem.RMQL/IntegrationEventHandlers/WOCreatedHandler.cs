using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.RMQL.IntegrationEventHandlers.Abstractions;

using Microsoft.Extensions.Logging;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    internal class WOCreatedHandler<TEntity>(
        ILogger<WOCreatedCRUDEvent<TEntity>> logger,
        IServiceProvider services
        ) : AbstractCRUDHandler<TEntity, WOCreatedCRUDEvent<TEntity>>(logger, services)
        where TEntity : IEntityDB
    {
        protected override string ThisName => "WOCreatedHandler";
        protected override string GenerateScript(WOCreatedCRUDEvent<TEntity> @event, IEntityDenormalizer<TEntity> denormalizer)
        {
            string script = "";
            foreach (var entity in @event.Entities) {
                script += denormalizer.Append(entity);
            }
            return script;
        }
    }
}
