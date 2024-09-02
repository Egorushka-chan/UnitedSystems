using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using RabbitMQ.Client;

using UnitedSystems.EventBus.Interfaces;

namespace UnitedSystems.EventBus.RabbitMQ
{
    public static class RabbitMQInjector
    {
        private const string SettingPath = "EventBus";

        public static IEventBusBuilder AddRabbitMQEventBus(this IHostApplicationBuilder builder, string connectionString)
        {
            builder.Services.Configure<RabbitEventBusSettings>(builder.Configuration.GetSection(SettingPath));

            builder.Services.AddSingleton(opt => {
                RabbitEventBusSettings configuration = opt.GetRequiredService<IOptions<RabbitEventBusSettings>>().Value;

                ConnectionFactory connectionFactory = new() {
                    DispatchConsumersAsync = true,
                    UserName = configuration.UserName,
                    Password = configuration.Password,
                    HostName = connectionString
                };

                return connectionFactory.CreateConnection(connectionString);
            });

            builder.Services.AddSingleton<IEventBus, RabbitMQEventBus>();
            builder.Services.AddSingleton<IHostedService>(opt => (RabbitMQEventBus)opt.GetRequiredService<IEventBus>());

            return new RabbitMQEventBusBuilder(builder.Services);
        }
    }
}
