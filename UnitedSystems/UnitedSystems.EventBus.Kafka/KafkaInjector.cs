using Confluent.Kafka;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using UnitedSystems.EventBus.Interfaces;

namespace UnitedSystems.EventBus.Kafka
{
    public static class KafkaInjector
    {
        private const string SettingPath = "EventBus";
        public static IEventBusBuilder AddKafkaEventBus(this IHostApplicationBuilder builder)
        {
            builder.Services.Configure<KafkaEventBusSettings>(builder.Configuration.GetSection(SettingPath));

            builder.Services.AddSingleton<IEventBus, KafkaEventBus>();
            builder.Services.AddSingleton<IHostedService>(opt => (KafkaEventBus)opt.GetRequiredService<IEventBus>());

            return new KafkaEventBusBuilder(builder);
        }
    }
}
