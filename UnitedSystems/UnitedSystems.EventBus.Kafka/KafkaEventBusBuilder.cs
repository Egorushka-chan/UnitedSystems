using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using UnitedSystems.EventBus.Interfaces;

namespace UnitedSystems.EventBus.Kafka
{
    internal class KafkaEventBusBuilder(IHostApplicationBuilder builder) : IEventBusBuilder
    {
        public IServiceCollection Services { get; set; } = builder.Services;
    }
}
