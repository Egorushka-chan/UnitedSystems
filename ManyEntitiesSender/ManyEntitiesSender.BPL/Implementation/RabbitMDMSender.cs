using System.Text;
using System.Text.Json;

using ManyEntitiesSender.BPL.Abstraction;
using ManyEntitiesSender.PL.Settings;

using RabbitMQ.Client;

using UnitedSystems.CommonLibrary.Queries;

namespace ManyEntitiesSender.BPL.Implementation
{
    public class RabbitMDMSender : IMDMSender
    {
        private QueueType queueType = QueueType.MESToMDM;
        private readonly BrokerSettings brokerSettings;
        private readonly IConnectionFactory factory;
        private readonly IConnection connection;
        private readonly IModel channel;
        internal RabbitMDMSender(BrokerSettings option, IConnectionFactory connectionFactory) 
        {
            this.factory = connectionFactory;
            brokerSettings = option;

            connection = factory.CreateConnection(brokerSettings.ConnectionString);
            channel = connection.CreateModel();
        }

        public void Send(string message)
        {
            var queue = channel.QueueDeclare(
                    queue: QueueEnumConverter.GetChannelName(queueType),
                    durable: false,
                    exclusive: false);
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("", routingKey: queue.QueueName, body: body);
        }

        public void Send(object obj)
        {
            string serial = JsonSerializer.Serialize(obj);
            Send(serial);
        }
    }
}
