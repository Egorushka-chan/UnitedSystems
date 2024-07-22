using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDominaSystem.BLL.Services.Abstractions
{
    public interface IGeneralInfoProvider
    {
        List<string> MessagePool { get; set; }
    }
}
