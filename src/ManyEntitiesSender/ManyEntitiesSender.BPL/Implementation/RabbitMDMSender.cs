using System.Text;
using System.Text.Json;

using ManyEntitiesSender.BPL.Abstraction;
using ManyEntitiesSender.PL.Settings;

using Microsoft.Extensions.Options;

using RabbitMQ.Client;

using UnitedSystems.CommonLibrary.Models.ManyEntitiesSender.Messages.Headers;
using UnitedSystems.CommonLibrary.Models.MasterDominaSystem.Messages;
using UnitedSystems.CommonLibrary.Queries;
using UnitedSystems.CommonLibrary.Queries.Interfaces;

namespace ManyEntitiesSender.BPL.Implementation
{
    public class RabbitMDMSender<TQueue> : IMDMSender
        where TQueue : IQueueInfo
    {
        private readonly IConnectionFactory factory;
        private readonly IConnection connection;
        private readonly IModel channel;
        public RabbitMDMSender(IConnectionFactory connectionFactory, IOptions<BrokerSettings> settings) 
        {
            this.factory = connectionFactory;

            connection = factory.CreateConnection(settings.Value.ConnectionString);
            channel = connection.CreateModel();
            channel.QueueDeclare(
                queue: TQueue.GetQueueKey(),
                durable: false,
                exclusive: false);
        }

        public void Send(string message, MessageHeaderFromMES header = MessageHeaderFromMES.NotSpecified)
        {
            MessageFromMES messageFromMES = new() {
                Body = message,
                Type = header
            };
            string serial = JsonSerializer.Serialize(messageFromMES);
            var body = Encoding.UTF8.GetBytes(serial);
            channel.BasicPublish("", routingKey: TQueue.GetQueueKey(), body: body);
        }

        public void Send(object obj, MessageHeaderFromMES header = MessageHeaderFromMES.NotSpecified)
        {
            string serial = JsonSerializer.Serialize(obj);
            Send(serial, header);
        }
    }
}
