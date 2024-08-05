using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedSystems.CommonLibrary.Messages
{
    /// <summary>
    /// И сам этот класс, и классы наследуемые от него используются в брокере сообщений как сообщение
    /// </summary>
    [Obsolete("Старая версия для старого брокера. Теперь используйте IntegrationEvent")]
    public class BrokerInfoObject
    {
        public string Summary { get; set; } = "NaN";
    }
}
