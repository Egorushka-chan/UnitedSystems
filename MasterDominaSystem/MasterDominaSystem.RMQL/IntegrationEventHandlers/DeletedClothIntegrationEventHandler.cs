using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    [Obsolete("Бесполезный класс, в текущей бизнес логике, его функционал спокойно заменяет WODeletedCRUDEvent")]
    internal class DeletedClothIntegrationEventHandler
        : IIntegrationEventHandler<DeletedClothIntegrationEvent>
    {
        public Task Handle(DeletedClothIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
