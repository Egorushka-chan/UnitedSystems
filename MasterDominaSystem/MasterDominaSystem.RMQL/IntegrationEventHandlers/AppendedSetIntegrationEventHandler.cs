using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    internal class AppendedSetIntegrationEventHandler :
        IIntegrationEventHandler<AppendedSetIntegrationEvent>
    {
        public Task Handle(AppendedSetIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
