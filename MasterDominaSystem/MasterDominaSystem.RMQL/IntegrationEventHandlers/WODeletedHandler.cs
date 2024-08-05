using MasterDominaSystem.BLL.Services.Abstractions;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    public class WODeletedHandler<TEntity>(ISessionInfoProvider sessionInfo) 
        : IIntegrationEventHandler<WODeletedCRUDEvent<TEntity>>
        where TEntity : IEntity
    {
        public Task Handle(WODeletedCRUDEvent<TEntity> @event)
        {
            sessionInfo.DeleteRequestWO.Add($"{typeof(TEntity).Name}: id: {@event.EntitiesIDs.First()}");
            return Task.CompletedTask;
        }
    }
}