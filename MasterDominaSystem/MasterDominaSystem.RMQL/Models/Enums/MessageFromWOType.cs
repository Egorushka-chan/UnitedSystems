using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDominaSystem.RMQL.Models.Enums
{
    public enum MessageFromWOType
    {
        NotSpecified = 0,
        AppStart = 1,
        AppClose = 2,
        Post = 3,
        Put = 4,
        Get = 5,
        Delete = 6
    }
}
