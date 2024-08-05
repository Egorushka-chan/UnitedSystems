using UnitedSystems.CommonLibrary.Messages;

namespace UnitedSystems.EventBus.Interfaces
{
    public interface IEventBus
    {
        Task PublishAsync(IntegrationEvent @event);
    }
}
