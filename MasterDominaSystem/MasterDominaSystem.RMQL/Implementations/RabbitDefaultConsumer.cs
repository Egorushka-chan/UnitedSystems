using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using MasterDominaSystem.RMQL.Models.Enums;
using MasterDominaSystem.RMQL.Models.Messages;

using Microsoft.Extensions.DependencyInjection;

using RabbitMQ.Client;

namespace MasterDominaSystem.RMQL.Implementations
{
    public class RabbitDefaultConsumer<TMessage> : AsyncDefaultBasicConsumer where TMessage : IBrokerMessage<TMessage>
    {
        private readonly IServiceProvider services;
        public RabbitDefaultConsumer(IModel model, IServiceProvider servicesProvider) : base(model)
        {
            services = servicesProvider;
        }

        public override Task HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            byte[] bodyArray = body.ToArray();
            string json = Encoding.UTF8.GetString(bodyArray);
            TMessage messageObject = JsonSerializer.Deserialize<TMessage>(json) ?? throw new ArgumentNullException(nameof(messageObject), "Empty package received");
            messageObject.Handle(services);
            return Task.CompletedTask;
        }

        public override Task OnCancel(params string[] consumerTags)
        {
            return base.OnCancel(consumerTags);
        }
    }
}
