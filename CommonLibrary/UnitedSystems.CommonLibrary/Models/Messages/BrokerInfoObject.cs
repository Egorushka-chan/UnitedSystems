using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedSystems.CommonLibrary.Models.Messages
{
    /// <summary>
    /// И сам этот класс, и классы наследуемые от него используются в брокере сообщений как сообщение
    /// </summary>
    public class BrokerInfoObject
    {
        public string Summary { get; set; } = "NaN";
    }
}
