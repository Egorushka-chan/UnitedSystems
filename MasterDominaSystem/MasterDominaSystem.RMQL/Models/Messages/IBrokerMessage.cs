using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace MasterDominaSystem.RMQL.Models.Messages
{
    public interface IBrokerMessage<TValue> where TValue : IBrokerMessage<TValue>
    {
        string Body { get; set; }

        void Handle(IServiceProvider services);
    }
}
