using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDominaSystem.RMQL.Abstractions
{
    public interface IBrokerConsumer
    {
        Task<string> GetMessage();
    }
}
