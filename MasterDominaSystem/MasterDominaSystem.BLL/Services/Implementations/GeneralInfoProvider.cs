using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MasterDominaSystem.BLL.Services.Abstractions;

namespace MasterDominaSystem.BLL.Services.Implementations
{
    public class GeneralInfoProvider : IGeneralInfoProvider
    {
        public List<string> MessagePool { get; set; }
    }
}
