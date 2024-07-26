﻿using ManyEntitiesSender.BPL.Abstraction;
using ManyEntitiesSender.BPL.Implementation;
using ManyEntitiesSender.PL.Settings;

using MasterDominaSystem.RMQL.Models.Queues;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RabbitMQ.Client;

namespace ManyEntitiesSender.BPL
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
            services.AddSingleton<IMDMSender, RabbitMDMSender<QueueInfoMES>>();
        }
    }
}
