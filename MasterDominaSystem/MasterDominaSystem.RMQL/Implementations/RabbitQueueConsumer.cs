using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

using MasterDominaSystem.RMQL.Abstractions;
using MasterDominaSystem.RMQL.Models;

using RabbitMQ.Client;

namespace MasterDominaSystem.RMQL.Implementations
{
    public class RabbitQueueConsumer : IBrokerConsumer
    {
        ConnectionFactory factory = new ConnectionFactory() {
            HostName = "RabbitMQ"
        };

        IConnection connection;
        IModel channel;
        private bool disposedValue;

        public RabbitQueueConsumer(ConnectionFactory factory, RabbitQueueType rabbitQueue)
        {
            this.factory = factory;
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            ConfigureQueues(rabbitQueue);
        }

        private void ConfigureQueues(RabbitQueueType rabbitQueue)
        {
            channel.QueueDeclare(
                queue: RabbitDescriptor.GetQueueName(rabbitQueue),
                durable: false,
                exclusive: false,
                autoDelete: false);
        }

        public Task<string> HandleMessage(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue) {
                if (disposing) {
                    if (connection != null) connection.Dispose();
                    if (channel != null) channel.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
