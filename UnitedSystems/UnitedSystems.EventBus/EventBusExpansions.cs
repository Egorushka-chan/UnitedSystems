using Microsoft.Extensions.DependencyInjection;
using UnitedSystems.CommonLibrary.Messages;
using UnitedSystems.EventBus.Interfaces;
using UnitedSystems.EventBus.Models;

namespace UnitedSystems.EventBus
{
    public static class EventBusExpansions
    {
        public static IEventBusBuilder AddSubscription<TIntegration, THandler>(this IEventBusBuilder builder, ServiceLifetime contextLifeTime = ServiceLifetime.Singleton)
            where TIntegration : IntegrationEvent
            where THandler : class, IIntegrationEventHandler<TIntegration>
        {
            // Регистрирую сервис по ключу из типа интеграционного ивента
            // консьюмер сможет использовать IKeyedServiceProvider.GetKeyedService<IIntegrationEventHandler>(typeof(T)) 
            // что бы получить все подходящие обработчики для данного типа

            builder.Services.AddKeyedSingleton<IIntegrationEventHandler, THandler>(typeof(TIntegration));

            //switch (contextLifeTime)
            //{
            //    case ServiceLifetime.Singleton:
            //        builder.Services.AddKeyedSingleton<THandler>(typeof(TIntegration));
            //        break;
            //    case ServiceLifetime.Scoped:
            //        builder.Services.AddKeyedScoped<THandler>(typeof(TIntegration));
            //        break;
            //    case ServiceLifetime.Transient:
            //        builder.Services.AddKeyedTransient<THandler>(typeof(TIntegration));
            //        break;
            //}

            builder.Services.Configure<EventBusSubscriptionInfo>(o =>
            {
                // Используется в брокере при привязке каналов
                o.EventTypes[typeof(TIntegration).GetKey()] = typeof(TIntegration);
            });

            return builder;
        }
    }
}
