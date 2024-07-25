using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDominaSystem.RMQL.Models.Queues.Interface
{
    internal interface IQueueInfo
    {
        abstract static string GetQueueKey();
    }
}
