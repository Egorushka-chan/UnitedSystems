using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    internal class DeletedSetIntegrationEventHandler
        : IIntegrationEventHandler<DeletedSetIntegrationEvent>
    {
        public Task Handle(DeletedSetIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
