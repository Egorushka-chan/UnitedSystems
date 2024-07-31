using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using UnitedSystems.EventBus.Interfaces;

namespace UnitedSystems.EventBus.RabbitMQ
{
    public class RabbitMQEventBus
        (IOptions<EventBusSettings> busSettings,
        IServiceProvider serviceProvider,
        ILogger<RabbitMQEventBus> logger) 
        : BackgroundService, IEventBus, IDisposable
    {
        public Task PublishAsync()
        {
            throw new NotImplementedException();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Factory.StartNew(() => {

            }, stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);

            return Task.CompletedTask;
        }
    }
}
