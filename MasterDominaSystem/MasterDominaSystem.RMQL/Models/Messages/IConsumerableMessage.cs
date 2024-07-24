using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace MasterDominaSystem.RMQL.Models.Messages
{
    /// <summary>
    /// Обертка над сообщениями
    /// </summary>
    public interface IConsumerableMessage
    {
        /// <summary>
        /// Выполнить действия исходя из содержания сообщения
        /// </summary>
        /// <remarks>
        /// Может показаться странным что обработка сообщений вынесена в сообщение - возможно.
        /// Но иначе пришлось или нарушать принципы ООП, или делать ещё один класс-обертку (зачем?)
        /// </remarks>
        void Handle(IServiceProvider services);
    }
}
