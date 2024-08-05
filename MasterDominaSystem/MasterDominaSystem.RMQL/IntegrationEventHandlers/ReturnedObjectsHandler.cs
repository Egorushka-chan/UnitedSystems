using MasterDominaSystem.BLL.Services.Abstractions;

using UnitedSystems.CommonLibrary.ManyEntitiesSender.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    public class ReturnedObjectsHandler(ISessionInfoProvider generalInfoProvider) : IIntegrationEventHandler<MESReturnedObjectsEvent>
    {
        public Task Handle(MESReturnedObjectsEvent @event)
        {
            generalInfoProvider.GetRequestsMES.Add(@event);

            return Task.CompletedTask;
        }
    }
}
