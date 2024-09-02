using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.RMQL.IntegrationEventHandlers.Abstractions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    internal class AppendedSetIntegrationEventHandler(
        ILogger<AppendedPersonIntegrationEventHandler> logger,
        IServiceProvider serviceProvider
        ) : AbstractIntegrationHandler<AppendedSetIntegrationEvent>(logger, serviceProvider)
    {
        public override string ThisName => nameof(AppendedSetIntegrationEventHandler);

        public override async Task<string> GenerateScript(AppendedSetIntegrationEvent @event)
        {
            _logger.LogTrace("Получение скрипта для Set - ID:{id}, Name:{name}", @event.Set.ID, @event.Set.Name);
            var setDenormalizer = (IEntityDenormalizer<Set>)_services.GetRequiredKeyedService<IEntityDenormalizer>(typeof(Set).GetKey());
            string setScript = await setDenormalizer.Append(@event.Set);
            _logger.LogTrace("Получен скрипт:\n{script}", setScript);

            string seasonScript = "";
            if (@event.Season != null) {
                seasonScript += await CreateScript(new[] { @event.Season });
            }

            string setHasClothesScript = await CreateScript(@event.SetHasClothes);

            string unitedScript = setScript + seasonScript + setHasClothesScript;
            return unitedScript;
        }
    }
}
