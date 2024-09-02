using MasterDominaSystem.RMQL.IntegrationEventHandlers;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using UnitedSystems.CommonLibrary.ManyEntitiesSender.IntegrationEvents;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus;
using UnitedSystems.EventBus.Interfaces;
using UnitedSystems.EventBus.RabbitMQ;
using UnitedSystems.EventBus.Kafka;

namespace MasterDominaSystem.RMQL
{
    public static class InjectorRMQL
    {
        private const string RabbitConnectionString = "RabbitMQ";
        public static IHostApplicationBuilder InjectRMQL(this IHostApplicationBuilder builder)
        {
            builder.SelectEventBus()
                // ManyEntitiesSender
                .AddSubscription<MESReturnedObjectsEvent, ReturnedObjectsHandler>()
                // WardrobeOnline
                .AddWOSubscriptionCRUD<Cloth>()
                .AddWOSubscriptionCRUD<Physique>()
                .AddWOSubscriptionCRUD<Set>()
                .AddWOSubscriptionCRUD<Person>()

                .AddDenormalizationSubscriptions();

            return builder;
        }

        private static IEventBusBuilder AddWOSubscriptionCRUD<TEntity>(this IEventBusBuilder builder) where TEntity : IEntityDB
        {
            builder.AddSubscription<WOCreatedCRUDEvent<TEntity>, WOCreatedHandler<TEntity>>()
                .AddSubscription<WODeletedCRUDEvent<TEntity>, WODeletedHandler<TEntity>>()
                .AddSubscription<WOUpdatedCRUDEvent<TEntity>, WOUpdatedHandler<TEntity>>();
            return builder;
        }

        private static IEventBusBuilder AddDenormalizationSubscriptions(this IEventBusBuilder builder)
        {
            builder.AddSubscription<AppendedClothIntegrationEvent, AppendedClothIntegrationEventHandler>();
            builder.AddSubscription<AppendedPersonIntegrationEvent, AppendedPersonIntegrationEventHandler>();
            builder.AddSubscription<AppendedSetIntegrationEvent, AppendedSetIntegrationEventHandler>();
            return builder;
        }

        private static IEventBusBuilder SelectEventBus(this IHostApplicationBuilder builder)
        {
            if (builder.IsPreferRabbitMQ()) {
                return builder.AddRabbitMQEventBus(RabbitConnectionString);
            }
            else {
                return builder.AddKafkaEventBus();
            }
        }

        private static bool IsPreferRabbitMQ(this IHostApplicationBuilder builder)
        {
            string errorMessage = "PreferRabbitMQOverKafka must be 1,0,true,false";
            bool preferRabbit = false;

            string? value = builder.Configuration["PreferRabbitMQOverKafka"];
            if (value != null) {
                bool converted = int.TryParse(value, out int number);
                if (converted) {
                    switch (number) {
                        case 0:
                            preferRabbit = false;
                            break;
                        case 1:
                            preferRabbit = true;
                            break;
                        default:
                            throw new InvalidOperationException(errorMessage);
                    }
                }
                else {
                    converted = bool.TryParse(value, out bool second);
                    if (converted) {
                        preferRabbit = second;
                    }
                    else {
                        throw new InvalidOperationException(errorMessage);
                    }
                }
            }

            return preferRabbit;
        }
    }
}
