﻿using MasterDominaSystem.RMQL.Implementations;
using MasterDominaSystem.RMQL.Models.Messages;
using MasterDominaSystem.RMQL.Models.Queues.Interface;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;

namespace MasterDominaSystem.RMQL.Workers
{
    internal class RabbitListener<TQueueInfo, TConsumableMessage>
        (IServiceProvider serviceProvider, ILogger<RabbitListener<TQueueInfo, TConsumableMessage>> logger) : BackgroundService
        where TQueueInfo : IQueueInfo
        where TConsumableMessage : IConsumerableMessage
    {
        private readonly ILogger<RabbitListener<TQueueInfo, TConsumableMessage>> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            IConnectionFactory factory = _serviceProvider.GetRequiredService<IConnectionFactory>();

            using var connection = factory.CreateConnection("RabbitMQ");

            var consumeChannel = connection.CreateModel();
            string queueKey = TQueueInfo.GetQueueKey();

            while (!stoppingToken.IsCancellationRequested) 
            {
                var eventConsumer = new RabbitEventConsumer<TConsumableMessage>(consumeChannel, _serviceProvider);
                consumeChannel.QueueDeclare(
                    queue: queueKey,
                    durable: false,
                    exclusive: true,
                    autoDelete: true);
                consumeChannel.BasicConsume(queueKey, true, eventConsumer);

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
