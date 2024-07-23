using MasterDominaSystem.RMQL.Workers;

using Microsoft.Extensions.DependencyInjection;

using RabbitMQ.Client;

namespace MasterDominaSystem.RMQL
{
    public static class InjectorRMQL
    {
        public static void InjectRMQL(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddHostedService<RabbitListener>();
        }
    }
}
