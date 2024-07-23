using MasterDominaSystem.RMQL.Implementations;
using MasterDominaSystem.RMQL.Models.Messages;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;

namespace MasterDominaSystem.RMQL.Workers
{
    public class RabbitListener(IServiceProvider serviceProvider, ILogger<RabbitListener> logger) : BackgroundService
    {
        private readonly ILogger<RabbitListener> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            IConnectionFactory factory = _serviceProvider.GetRequiredService<IConnectionFactory>();

            using var connection = factory.CreateConnection("RabbitMQ");

            var consumerMES = new RabbitDefaultConsumer<MessageFromMES>(connection.CreateModel(), _serviceProvider);
            var consumerWO = new RabbitEventConsumer<MessageFromMES>(connection.CreateModel(), _serviceProvider);

            while (!stoppingToken.IsCancellationRequested)
            {
                
            }
        }

        public override void Dispose()
        {

            base.Dispose();
        }
    }
}
