using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedSystems.EventBus.Interfaces
{
    public interface IEventBus
    {
        Task PublishAsync();
    }
}
