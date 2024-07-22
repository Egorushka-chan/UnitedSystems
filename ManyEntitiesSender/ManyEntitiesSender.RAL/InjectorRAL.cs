using ManyEntitiesSender.DAL.Implementations;
using ManyEntitiesSender.DAL.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace ManyEntitiesSender.RAL
{
    public static class InjectorRAL
    {
        public static void InjectRAL(this IServiceCollection services)
        {
            services.AddScoped<IRedisProvider, RedisProvider>();
        }
    }
}
