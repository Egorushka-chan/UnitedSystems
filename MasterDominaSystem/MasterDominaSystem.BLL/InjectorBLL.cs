using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.BLL.Services.Implementations;

using Microsoft.Extensions.DependencyInjection;

namespace MasterDominaSystem.BLL
{
    public static class InjectorBLL
    {
        public static void InjectBLL(this IServiceCollection services)
        {
            services.AddSingleton<IGeneralInfoProvider, GeneralInfoProvider>();
        }
    }
}
