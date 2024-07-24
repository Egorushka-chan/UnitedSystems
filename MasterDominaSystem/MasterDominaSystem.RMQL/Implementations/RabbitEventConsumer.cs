using System.Text;
using System.Text.Json;

using MasterDominaSystem.RMQL.Models.Messages;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MasterDominaSystem.RMQL.Implementations
{
    public class RabbitEventConsumer<TMessage> : EventingBasicConsumer where TMessage : IConsumerableMessage
    {
        private readonly IServiceProvider services;
        private readonly ILogger<RabbitEventConsumer<TMessage>> logger;
        public RabbitEventConsumer(IModel model, IServiceProvider servicesProvider) : base(model)
        {
            services = servicesProvider;
            logger = services.GetRequiredService<ILogger<RabbitEventConsumer<TMessage>>>();

            Received += EventConsumer_Received;
        }

        private void EventConsumer_Received(object? sender, BasicDeliverEventArgs messageArgs)
        {
            logger.LogTrace("Message received from {routingKey}", messageArgs.RoutingKey);
            byte[] bodyArray = messageArgs.Body.ToArray();
            string json = Encoding.UTF8.GetString(bodyArray);
            if (string.IsNullOrEmpty(json)) {
                logger.LogWarning("Consumer received empty body package" + $": sender: {sender}");
            }
            TMessage? messageObject = JsonSerializer.Deserialize<TMessage>(json);
            if (messageObject is null) {
                logger.LogError("Failed to deserialize {type} after receiving message: sender: {sender}", typeof(TMessage).Name, sender);
                throw new JsonException("Deserialization error");
            }
            messageObject.Handle(services);
        }
    }
}
