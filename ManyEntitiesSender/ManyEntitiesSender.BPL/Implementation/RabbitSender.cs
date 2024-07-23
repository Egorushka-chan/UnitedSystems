using System.Text;
using System.Text.Json;

using ManyEntitiesSender.BPL.Abstraction;
using ManyEntitiesSender.PL.Enums;
using ManyEntitiesSender.PL.Settings;

using RabbitMQ.Client;

namespace ManyEntitiesSender.BPL.Implementation
{
    internal class RabbitSender : IBrokerSender
    {
        private BrokerSettings brokerSettings;
        internal RabbitSender(BrokerSettings option) 
        {
            brokerSettings = option;
        }

        public void Send(string message, RabbitQueueType queueType)
        {
            var factory = new ConnectionFactory();
            
            using (var connection = factory.CreateConnection("RabbitMQ"))
            using (var channel = connection.CreateModel()) {
                var queue = channel.QueueDeclare(
                        queue: RabbitQueue.Name(queueType),
                        durable: false,
                        exclusive: false);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", routingKey: queue.QueueName, body: body);
            }
        }

        public void Send(object obj, RabbitQueueType queueType)
        {
            string serial = JsonSerializer.Serialize(obj);
            Send(serial, queueType);
        }
    }
}
