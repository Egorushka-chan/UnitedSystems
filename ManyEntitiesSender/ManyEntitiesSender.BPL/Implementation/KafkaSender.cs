using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ManyEntitiesSender.BPL.Abstraction;
using ManyEntitiesSender.PL.Enums;

namespace ManyEntitiesSender.BPL.Implementation
{
    public class KafkaSender : IBrokerSender
    {
        public void Send(string message, RabbitQueueType queueType)
        {
            throw new NotImplementedException();
        }

        public void Send(object obj, RabbitQueueType queueType)
        {
            throw new NotImplementedException();
        }
    }
}
