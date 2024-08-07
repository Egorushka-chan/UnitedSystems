using MasterDominaSystem.BLL.Services.Abstractions;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    public class WOUpdatedHandler<TEntity>(ISessionInfoProvider sessionInfo)
        : IIntegrationEventHandler<WOUpdatedCRUDEvent<TEntity>>
        where TEntity : IEntityDB
    {
        public Task Handle(WOUpdatedCRUDEvent<TEntity> @event)
        {
            sessionInfo.PutRequestsWO.Add($"{typeof(TEntity).Name}: id: {@event.Entities.First().ID}");
            return Task.CompletedTask;
        }
    }
}
