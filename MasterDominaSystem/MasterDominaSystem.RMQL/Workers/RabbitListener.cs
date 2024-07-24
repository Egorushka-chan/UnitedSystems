using MasterDominaSystem.RMQL.Implementations;
using MasterDominaSystem.RMQL.Models.Messages;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;

using UnitedSystems.CommonLibrary.Queries;

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

            var consumeChannel = connection.CreateModel();
            string mesQueue = QueueEnumConverter.GetChannelName(QueueType.MESToMDM);
            string woQueue = QueueEnumConverter.GetChannelName(QueueType.WOtoMDM);

            consumeChannel.QueueDeclare(
                queue: mesQueue,
                durable: false,
                exclusive: true,
                autoDelete: true);

            consumeChannel.QueueDeclare(
                queue: woQueue,
                durable: false,
                exclusive: true,
                autoDelete: true);


            while (!stoppingToken.IsCancellationRequested) 
            {
                var consumerMES = new RabbitEventConsumer<ConsumerableMessageFromMES>(connection.CreateModel(), _serviceProvider);
                var consumerWO = new RabbitEventConsumer<ConsumerableMessageFromWO>(connection.CreateModel(), _serviceProvider);

                consumeChannel.BasicConsume(mesQueue, true, consumerMES);
                consumeChannel.BasicConsume(woQueue, true, consumerWO);

                while (!stoppingToken.IsCancellationRequested)
                {

                }
            }
        }

        public override void Dispose()
        {

            base.Dispose();
        }
    }
}
