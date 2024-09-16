using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.RMQL.IntegrationEventHandlers.Abstractions;

using Microsoft.Extensions.Logging;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    internal class WOUpdatedHandler<TEntity>(
        ILogger<WOUpdatedHandler<TEntity>> logger,
        IServiceProvider services
        ) : AbstractCRUDHandler<TEntity, WOUpdatedCRUDEvent<TEntity>>(logger, services)
        where TEntity : IEntityDB
    {
        protected override string ThisName => "WOUpdatedHandler";
        protected override string GenerateScript(WOUpdatedCRUDEvent<TEntity> @event, IEntityDenormalizer<TEntity> denormalizer)
        {
            string script = "";
            foreach (var entity in @event.Entities) {
                script += denormalizer.Append(entity);
            }
            return script;
        }
    }
}
