using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MasterDominaSystem.RMQL.Models;

namespace MasterDominaSystem.RMQL.Abstractions
{
    public interface IBrokerConsumer : IDisposable
    {
        /// <summary>
        /// Этот метод будет вызываться каждый раз, когда приходит новый объект
        /// </summary>
        /// <returns></returns>
        Task<string> HandleMessage(CancellationToken cancellationToken = default);
    }
}
