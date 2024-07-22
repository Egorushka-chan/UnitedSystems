using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MasterDominaSystem.RMQL.Abstractions;

namespace MasterDominaSystem.RMQL.Implementations
{
    public class RabbitConsumer : IBrokerConsumer
    {
        public Task<string> GetMessage()
        {
            throw new NotImplementedException();
        }
    }
}
