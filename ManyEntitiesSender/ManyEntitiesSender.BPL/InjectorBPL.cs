using ManyEntitiesSender.BPL.Abstraction;
using ManyEntitiesSender.BPL.Implementation;

using Microsoft.Extensions.DependencyInjection;

namespace ManyEntitiesSender.BPL
{
    public static class InjectorBPL
    {
        public static void InjectBPL(this IServiceCollection services)
        {
            services.AddScoped<IBrokerSender, RabbitSender>();
        }
    }
}
