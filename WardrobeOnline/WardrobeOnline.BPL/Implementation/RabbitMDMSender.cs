using System.Text;
using System.Text.Json;

using Microsoft.Extensions.Options;

using RabbitMQ.Client;

using UnitedSystems.CommonLibrary.MasterDominaSystem.Messages;
using UnitedSystems.CommonLibrary.WardrobeOnline.Messages.Headers;
using UnitedSystems.CommonLibrary.Queries;
using UnitedSystems.CommonLibrary.Queries.Interfaces;

using WardrobeOnline.BPL.Abstractions;
using WardrobeOnline.BPL.Settings;

namespace WardrobeOnline.BPL.Implementations
{
    public class RabbitMDMSender<TQueue> : IMDMSender
        where TQueue : IQueueInfo
    {
        private QueueType queueType = QueueType.WOtoMDM;
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

        public void Send(string message, MessageHeaderFromWO header = MessageHeaderFromWO.NotSpecified)
        {
            MessageFromWO messageFromMES = new() {
                Body = message,
                Type = header
            };
            string serial = JsonSerializer.Serialize(messageFromMES);
            var body = Encoding.UTF8.GetBytes(serial);
            channel.BasicPublish("", routingKey: TQueue.GetQueueKey(), body: body);
        }

        public void Send(object obj, MessageHeaderFromWO header = MessageHeaderFromWO.NotSpecified)
        {
            string serial = JsonSerializer.Serialize(obj);
            Send(serial, header);
        }
    }
}
