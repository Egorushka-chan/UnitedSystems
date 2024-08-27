using System.Text;

using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers
{
    internal class AppendedClothIntegrationEventHandler(IServiceProvider serviceProvider) :
        IIntegrationEventHandler<AppendedClothIntegrationEvent>
    {
        private readonly ILogger<AppendedClothIntegrationEventHandler> _logger = serviceProvider.GetRequiredService<ILogger<AppendedClothIntegrationEventHandler>>();

        public async Task Handle(AppendedClothIntegrationEvent @event)
        {
            _logger.LogInformation("Начало выполнения метода Handle обработчика событий AppendedClothIntegrationEvent. " +
                "Событие: {id} {timestamp}",@event.ID, @event.TimeStamp);

            _logger.LogInformation("Получение скрипта для Cloth - ID:{id}, Name:{name}",@event.Cloth.ID, @event.Cloth.Name);
            var clothDenormalizer = (IEntityDenormalizer<Cloth>)serviceProvider.GetRequiredKeyedService<IEntityDenormalizer>(typeof(Cloth).GetKey());
            string clothScript = await clothDenormalizer.Append(@event.Cloth);
            _logger.LogInformation("Получен скрипт:\n{script}", clothScript);

            string photoScript = await CreateScript(@event.Photos);

            string clothHasMaterialsScript = await CreateScript(@event.ClothHasMaterials);

            string materials = await CreateScript(@event.Materials);

            string unitedScript = clothScript + photoScript + clothHasMaterialsScript + materials;
            _logger.LogInformation("Кол-во символов итогового скрипта: {length}", unitedScript.Length);

            using(var scope = serviceProvider.CreateScope())
            {
                MasterContext context = serviceProvider.GetRequiredService<MasterContext>();
                await context.Database.ExecuteSqlRawAsync(unitedScript);
            }
            
            _logger.LogInformation("Скрипт выполнен");
        }

        private async Task<string> CreateScript<TEntity>(ICollection<TEntity> entities) where TEntity : IEntityDB
        {
            string key = typeof(TEntity).GetKey();
            _logger.LogInformation("Получение скриптов для {key} у Cloth в количестве:{count}", key, entities.Count);
            StringBuilder script = new();
            foreach (TEntity chm in entities)
            {
                _logger.LogInformation("{key} - ID:{id}", key, chm.ID);
                var denormalizer = serviceProvider.GetRequiredKeyedService<IEntityDenormalizer>(typeof(TEntity).GetKey());
                string receivedScript = await denormalizer.Append(chm);
                _logger.LogInformation("Получен скрипт:\n{script}", receivedScript);
                script.Append(receivedScript);
            }
            return script.ToString();
        }
    }
}
