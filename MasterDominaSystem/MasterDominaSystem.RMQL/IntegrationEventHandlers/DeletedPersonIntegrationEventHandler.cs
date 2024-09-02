using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    [Obsolete("Бесполезный класс, в текущей бизнес логике, его функционал спокойно заменяет WODeletedCRUDEvent")]
    internal class DeletedPersonIntegrationEventHandler
        : IIntegrationEventHandler<DeletedPersonIntegrationEvent>
    {
        public Task Handle(DeletedPersonIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
