using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MasterDominaSystem.RMQL.Models.Enums;

namespace MasterDominaSystem.RMQL.Models
{
    public static class RabbitDescriptor
    {
        private static Dictionary<int, string> queues = new() {
            {1, "MESQueueToMDM" },
            {2, "WOQueueToMDM" }
        };

        public static string GetQueueName(RabbitQueueType rabbitQueue) => queues[(int)rabbitQueue];
    }

    
}
