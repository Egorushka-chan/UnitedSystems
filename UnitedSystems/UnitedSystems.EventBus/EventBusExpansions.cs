using Microsoft.Extensions.DependencyInjection;

using UnitedSystems.EventBus.Events;
using UnitedSystems.EventBus.Interfaces;

namespace UnitedSystems.EventBus
{
    public static class EventBusExpansions
    {
        public static IEventBusBuilder AddSubscription<TIntegration, THandler>(this IEventBusBuilder builder)
            where TIntegration : IntegrationEvent
            where THandler : class, IIntegrationEventHandler<TIntegration>
        {
            // Регистрирую сервис по ключу из типа интеграционного ивента
            // консьюмер сможет использовать IKeyedServiceProvider.GetKeyedService<IIntegrationEventHandler>(typeof(T)) 
            // что бы получить все подходящие обработчики для данного типа
            builder.Services.AddKeyedSingleton<IIntegrationEventHandler>(typeof(TIntegration));
            return builder;
        }
    }
}
