using ManyEntitiesSender.PL.Settings;

using MasterDominaSystem.RMQL.Models.Messages;
using MasterDominaSystem.RMQL.Models.Queues;
using MasterDominaSystem.RMQL.Workers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RabbitMQ.Client;

namespace MasterDominaSystem.RMQL
{
    public static class InjectorRMQL
    {
        public static void InjectRMQL(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory, ConnectionFactory>(opt =>
            {
                BrokerSettings configuration = opt.GetRequiredService<BrokerSettings>();

                return new ConnectionFactory()
                {
                    UserName = configuration.User,
                    Password = configuration.Password,
                    HostName = configuration.ConnectionString
                };
            });
            services.AddHostedService<RabbitListener<QueueInfoMES, ConsumerableMessageFromMES>>();
            services.AddHostedService<RabbitListener<QueueInfoWO, ConsumerableMessageFromWO>>();
        }
    }
}
