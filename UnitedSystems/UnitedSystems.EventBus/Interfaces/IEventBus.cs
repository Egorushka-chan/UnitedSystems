using UnitedSystems.EventBus.Events;

namespace UnitedSystems.EventBus.Interfaces
{
    public interface IEventBus
    {
        Task PublishAsync(IntegrationEvent @event);
    }
}
