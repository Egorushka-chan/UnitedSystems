using System.Text;

using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.RMQL.IntegrationEventHandlers.Abstractions;

using Microsoft.Extensions.Logging;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    internal class WODeletedHandler<TEntity>(
        ILogger<WOCreatedCRUDEvent<TEntity>> logger,
        IServiceProvider services
        ) : AbstractCRUDHandler<TEntity, WODeletedCRUDEvent<TEntity>>(logger, services)
        where TEntity : IEntityDB, new()
    {
        protected override string ThisName => "WODeletedHandler";

        protected override string GenerateScript(WODeletedCRUDEvent<TEntity> @event, IEntityDenormalizer<TEntity> denormalizer)
        {
            StringBuilder builder = new(127);
            foreach(int ID in @event.EntitiesIDs) {
                TEntity entity = new() {
                    ID = ID
                };
                builder.Append(denormalizer.Delete(entity));
            }
            return builder.ToString();
        }
    }
}