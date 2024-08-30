using Confluent.Kafka;

using Microsoft.Extensions.Hosting;

using UnitedSystems.EventBus.Interfaces;

namespace UnitedSystems.EventBus.Kafka
{
    public static class KafkaInjector
    {
        public static IEventBusBuilder AddKafkaEventBus(this IHostApplicationBuilder builder)
        {
            var producerConfig = new ProducerConfig();

            return new KafkaEventBusBuilder(builder);
        }
    }
}
