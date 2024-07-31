using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RabbitMQ.Client;

using UnitedSystems.CommonLibrary.Queries;

using WardrobeOnline.BPL.Abstractions;
using WardrobeOnline.BPL.Implementations;
using WardrobeOnline.BPL.Settings;

namespace WardrobeOnline.BPL
{
    public static class InjectorBPL
    {
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
            services.AddSingleton<IMDMSender, RabbitMDMSender<QueueInfoWO>>();
        }
    }
}
