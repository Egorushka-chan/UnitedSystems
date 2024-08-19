using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    internal class DeletedClothIntegrationEventHandler
        : IIntegrationEventHandler<DeletedClothIntegrationEvent>
    {
        public Task Handle(DeletedClothIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
