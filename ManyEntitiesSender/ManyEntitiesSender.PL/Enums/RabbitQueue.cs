using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyEntitiesSender.PL.Enums
{
    public static class RabbitQueue
    {
        private static Dictionary<int, string> SelectName = new() {
            {1, "MESQueueToMDM"}
        };

        public static string Name(RabbitQueueType type) => SelectName[(int)type];
    }

    public enum RabbitQueueType : int
    {
        MDM = 1
    }
}
