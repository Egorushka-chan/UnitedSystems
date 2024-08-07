using MasterDominaSystem.BLL.Services.Abstractions;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    public class WOCreatedHandler<TEntity>(ISessionInfoProvider sessionInfo)
        : IIntegrationEventHandler<WOCreatedCRUDEvent<TEntity>>
        where TEntity : IEntityDB
    {
        public Task Handle(WOCreatedCRUDEvent<TEntity> @event)
        {
            sessionInfo.PostRequestsWO.Add($"{typeof(TEntity).Name}: id: {@event.Entities.First().ID}");
            return Task.CompletedTask;
        }
    }
}
