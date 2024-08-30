using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using Confluent.Kafka;

using UnitedSystems.CommonLibrary.Messages;
using UnitedSystems.EventBus.Interfaces;
using UnitedSystems.EventBus.Models;

namespace UnitedSystems.EventBus.Kafka
{
    internal class KafkaEventBus(
        IOptions<KafkaEventBusSettings> busSettings,
        IServiceProvider services,
        IOptions<EventBusSubscriptionInfo> subscriptionsOptions
        ) : BackgroundService, IEventBus, IDisposable

    {
        private readonly KafkaEventBusSettings busSettings = busSettings.Value;
        private readonly IServiceProvider services = services;
        private readonly EventBusSubscriptionInfo subscriptionInfo = subscriptionsOptions.Value;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Factory.StartNew(() => {

                

            }, stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);

            return Task.CompletedTask;
        }

        public Task PublishAsync(IntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
