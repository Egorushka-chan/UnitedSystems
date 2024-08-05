using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.BLL.Services.Implementations;

using Microsoft.Extensions.DependencyInjection;

namespace MasterDominaSystem.BLL
{
    public static class InjectorBLL
    {
        public static IServiceCollection InjectBLL(this IServiceCollection services)
        {
            services.AddSingleton<ISessionInfoProvider, SessionInfoProvider>();
            return services;
        }
    }
}
