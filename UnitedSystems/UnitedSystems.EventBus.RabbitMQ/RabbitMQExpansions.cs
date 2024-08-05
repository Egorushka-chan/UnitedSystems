using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RabbitMQ.Client;

using UnitedSystems.EventBus.Interfaces;

namespace UnitedSystems.EventBus.RabbitMQ
{
    public static class RabbitMQExpansions
    {
        private static string SettingPath = "EventBus";

        public static IEventBusBuilder AddRabbitMQEventBus(this IHostApplicationBuilder builder, string connectionString)
        {
            builder.Services.Configure<EventBusSettings>(builder.Configuration.GetSection(SettingPath));

            builder.Services.AddSingleton(opt => {
                ConnectionFactory connectionFactory = new() {
                    DispatchConsumersAsync = true
                };
                return connectionFactory.CreateConnection(connectionString);
            });

            builder.Services.AddSingleton<IEventBus, RabbitMQEventBus>();
            builder.Services.AddSingleton<IHostedService>(opt => (RabbitMQEventBus)opt.GetRequiredService<IEventBus>());

            return new RabbitMQEventBusBuilder(builder.Services);
        }
    }
}
