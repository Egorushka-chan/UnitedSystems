using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    internal class AppendedPersonIntegrationEventHandler :
        IIntegrationEventHandler<AppendedPersonIntegrationEvent>
    {
        public Task Handle(AppendedPersonIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
