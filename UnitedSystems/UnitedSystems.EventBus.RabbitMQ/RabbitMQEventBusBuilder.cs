using Microsoft.Extensions.DependencyInjection;

using UnitedSystems.EventBus.Interfaces;

namespace UnitedSystems.EventBus.RabbitMQ
{
    /// <inheritdoc cref="IEventBusBuilder"/>
    public class RabbitMQEventBusBuilder(IServiceCollection services) : IEventBusBuilder
    {
        public IServiceCollection Services { get; set; } = services;
    }
}
