using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.RMQL.IntegrationEventHandlers.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    internal class AppendedPersonIntegrationEventHandler(
        ILogger<AppendedPersonIntegrationEventHandler> logger,
        IServiceProvider serviceProvider
        ) : AbstractIntegrationHandler<AppendedPersonIntegrationEvent>(logger, serviceProvider)
    {
        public override string ThisName => nameof(AppendedPersonIntegrationEventHandler);

        public override async Task<string> GenerateScript(AppendedPersonIntegrationEvent @event)
        {
            _logger.LogDebug("Получение скрипта для Person - ID:{id}, Name:{name}", @event.Person.ID, @event.Person.Name);
            var personDenormalizer = (IEntityDenormalizer<Person>)_services.GetRequiredKeyedService<IEntityDenormalizer>(typeof(Person).GetKey());
            string personScript = await personDenormalizer.Append(@event.Person);
            _logger.LogDebug("Получен скрипт:\n{script}", personScript);

            string physiquesScript = await CreateScript(@event.Physiques);

            string unitedScript = personScript + physiquesScript;
            return unitedScript;
        }
    }
}
