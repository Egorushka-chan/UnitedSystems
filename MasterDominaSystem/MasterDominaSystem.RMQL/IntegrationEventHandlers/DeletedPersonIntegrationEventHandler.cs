using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    internal class DeletedPersonIntegrationEventHandler
        : IIntegrationEventHandler<DeletedPersonIntegrationEvent>
    {
        public Task Handle(DeletedPersonIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
