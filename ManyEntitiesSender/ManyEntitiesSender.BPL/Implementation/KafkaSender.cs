using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ManyEntitiesSender.BPL.Abstraction;

namespace ManyEntitiesSender.BPL.Implementation
{
    public class KafkaSender : IBrokerSender
    {
        public void Send(string message)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(string message, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
