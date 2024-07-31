using MasterDominaSystem.RMQL.Models.Messages;
using MasterDominaSystem.RMQL.Models.Settings;
using MasterDominaSystem.RMQL.Workers;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RabbitMQ.Client;

using UnitedSystems.CommonLibrary.Queries;

namespace MasterDominaSystem.RMQL
{
    public static class InjectorRMQL
    {
        public static IServiceCollection InjectRMQL(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory, ConnectionFactory>(opt =>
            {
                BrokerSettings configuration = opt.GetRequiredService<IOptions<BrokerSettings>>().Value;

                return new ConnectionFactory()
                {
                    UserName = configuration.User,
                    Password = configuration.Password,
                    HostName = configuration.ConnectionString
                };
            });
            services.AddHostedService<RabbitListener<QueueInfoMES, ConsumerableMessageFromMES>>();
            services.AddHostedService<RabbitListener<QueueInfoWO, ConsumerableMessageFromWO>>();
            return services;
        }
    }
}
