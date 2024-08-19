using MasterDominaSystem.RMQL.IntegrationEventHandlers;

using Microsoft.Extensions.Hosting;

using UnitedSystems.CommonLibrary.ManyEntitiesSender.IntegrationEvents;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus;
using UnitedSystems.EventBus.Interfaces;
using UnitedSystems.EventBus.RabbitMQ;

namespace MasterDominaSystem.RMQL
{
    public static class InjectorRMQL
    {
        public static IHostApplicationBuilder InjectRMQL(this IHostApplicationBuilder builder, string connectionString)
        {
            builder.AddRabbitMQEventBus(connectionString)
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

            builder.AddSubscription<DeletedClothIntegrationEvent, DeletedClothIntegrationEventHandler>();
            builder.AddSubscription<DeletedPersonIntegrationEvent, DeletedPersonIntegrationEventHandler>();
            builder.AddSubscription<DeletedSetIntegrationEvent, DeletedSetIntegrationEventHandler>();
            return builder;
        }
    }
}
