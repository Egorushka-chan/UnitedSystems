using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnitedSystems.EventBus.Events;

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

        Task IIntegrationEventHandler.Handle(IntegrationEvent @event) => Handle(@event);
    }
}
