using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.RMQL.IntegrationEventHandlers.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    internal class AppendedClothIntegrationEventHandler(
        ILogger<AppendedClothIntegrationEventHandler> logger,
        IServiceProvider serviceProvider
        ) : AbstractIntegrationHandler<AppendedClothIntegrationEvent>(logger, serviceProvider)
    {
        public override string ThisName => nameof(AppendedClothIntegrationEventHandler);

        public override async Task<string> GenerateScript(AppendedClothIntegrationEvent @event)
        {
            _logger.LogDebug("Получение скрипта для Cloth - ID:{id}, Name:{name}", @event.Cloth.ID, @event.Cloth.Name);
            var clothDenormalizer = (IEntityDenormalizer<Cloth>)_services.GetRequiredKeyedService<IEntityDenormalizer>(typeof(Cloth).GetKey());

            if (@event.Cloth.ClothHasMaterials.Count == 0) {
                foreach (var chm in @event.ClothHasMaterials.Where(chm => chm.ClothID == @event.Cloth.ID)) {
                    @event.Cloth.ClothHasMaterials.Add(chm);
                };
            }
            if (@event.Cloth.Photos.Count == 0) {
                foreach (var photo in @event.Photos) {
                    @event.Cloth.Photos.Add(photo);
                };
            }

            string clothScript = await clothDenormalizer.Append(@event.Cloth);
            _logger.LogDebug("Получен скрипт:\n{script}", clothScript);

            string photoScript = await CreateScript(@event.Photos);

            string materials = await CreateScript(@event.Materials);

            string clothHasMaterialsScript = await CreateScript(@event.ClothHasMaterials);

            string unitedScript = clothScript + photoScript + materials + clothHasMaterialsScript;
            return unitedScript;
        }
    }
}
