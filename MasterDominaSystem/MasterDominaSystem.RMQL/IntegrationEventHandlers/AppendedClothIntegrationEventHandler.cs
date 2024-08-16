using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    public class AppendedClothIntegrationEventHandler :
        IIntegrationEventHandler<AppendedClothIntegrationEvent>
    {
        public Task Handle(AppendedClothIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
