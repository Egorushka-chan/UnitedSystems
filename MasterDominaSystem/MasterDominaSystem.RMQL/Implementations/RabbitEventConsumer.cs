using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using MasterDominaSystem.RMQL.Models.Messages;

using Microsoft.Extensions.DependencyInjection;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MasterDominaSystem.RMQL.Implementations
{
    public class RabbitEventConsumer<TMessage> where TMessage : IBrokerMessage<TMessage>
    {
        private readonly IModel channel;
        private readonly IServiceProvider services;
        private readonly AsyncEventingBasicConsumer eventConsumer;
        public RabbitEventConsumer(IModel model, IServiceProvider servicesProvider)
        {
            channel = model;
            services = servicesProvider;

            eventConsumer = new AsyncEventingBasicConsumer(model);
            eventConsumer.Received += EventConsumer_Received;
        }

        private Task EventConsumer_Received(object sender, BasicDeliverEventArgs messageArgs)
        {
            byte[] bodyArray = messageArgs.Body.ToArray();
            string json = Encoding.UTF8.GetString(bodyArray);
            TMessage messageObject = JsonSerializer.Deserialize<TMessage>(json) ?? throw new ArgumentNullException(nameof(messageObject), "Empty package received");
            messageObject.Handle(services);
            return Task.CompletedTask;
        }
    }
}
