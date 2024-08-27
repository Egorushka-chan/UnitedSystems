using UnitedSystems.CommonLibrary.Messages;

namespace UnitedSystems.EventBus.Interfaces
{
    public interface IIntegrationEventHandler
    {
        Task Handle(IntegrationEvent @event);
    }

    public interface IIntegrationEventHandler<TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);

        Task IIntegrationEventHandler.Handle(IntegrationEvent @event) => Handle((TIntegrationEvent)@event);
    }
}
