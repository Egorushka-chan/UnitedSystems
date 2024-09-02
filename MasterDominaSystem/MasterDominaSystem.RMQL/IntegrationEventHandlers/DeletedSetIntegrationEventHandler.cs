using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    [Obsolete("Бесполезный класс, в текущей бизнес логике, его функционал спокойно заменяет WODeletedCRUDEvent")]
    internal class DeletedSetIntegrationEventHandler
        : IIntegrationEventHandler<DeletedSetIntegrationEvent>
    {
        public Task Handle(DeletedSetIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
