using ManyEntitiesSender.BPL.Abstraction;
using ManyEntitiesSender.BPL.Implementation;
using ManyEntitiesSender.PL.Settings;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RabbitMQ.Client;

using UnitedSystems.CommonLibrary.Queries;

namespace ManyEntitiesSender.BPL
{
    public static class InjectorBPL
    {
        [Obsolete("Необходимо использовать библиотеку UnitedSystems.EventBus, это работать не будет")]
        public static void InjectBPL(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory, ConnectionFactory>(opt => {
                BrokerSettings configuration = opt.GetRequiredService<IOptions<BrokerSettings>>().Value;

                return new ConnectionFactory() {
                    UserName = configuration.User,
                    Password = configuration.Password,
                    HostName = configuration.ConnectionString
                };
            });
            services.AddSingleton<IMDMSender, RabbitMDMSender<QueueInfoMES>>();
        }
    }
}
