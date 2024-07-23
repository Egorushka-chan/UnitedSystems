using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MasterDominaSystem.RMQL.Models;

using RabbitMQ.Client.Events;

namespace MasterDominaSystem.RMQL.Abstractions
{
    public interface IBrokerConsumer : IDisposable
    {
        /// <summary>
        /// Этот метод будет вызываться каждый раз, когда приходит новый объект
        /// </summary>
        /// <returns></returns>
        void ConfigureMessageHandler(EventHandler<BasicDeliverEventArgs> handler);
    }
}
