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
    internal class AppendedSetIntegrationEventHandler(IServiceProvider serviceProvider) :
        IIntegrationEventHandler<AppendedSetIntegrationEvent>
    {
        private readonly ILogger<AppendedSetIntegrationEventHandler> _logger = serviceProvider.GetRequiredService<ILogger<AppendedSetIntegrationEventHandler>>();
        public async Task Handle(AppendedSetIntegrationEvent @event)
        {
            _logger.LogTrace("Начало выполнения метода Handle обработчика событий AppendedSetIntegrationEventHandler. " +
                "Событие: {id} {timestamp}",@event.ID, @event.TimeStamp);

            _logger.LogTrace("Получение скрипта для Set - ID:{id}, Name:{name}",@event.Set.ID, @event.Set.Name);
            var setDenormalizer = (IEntityDenormalizer<Set>)serviceProvider.GetRequiredKeyedService<IEntityDenormalizer>(typeof(Set).GetKey());
            string setScript = await setDenormalizer.Append(@event.Set);
            _logger.LogTrace("Получен скрипт:\n{script}", setScript);
            
            string seasonScript = "";
            if(@event.Season != null)
            {
                seasonScript += await CreateScript(new [] { @event.Season });
            }

            string setHasClothesScript = await CreateScript(@event.SetHasClothes);

            string unitedScript = setScript + seasonScript + setHasClothesScript;
            _logger.LogTrace("Кол-во символов итогового скрипта: {length}", unitedScript.Length);

            using(var scope = serviceProvider.CreateScope())
            {
                MasterContext context = serviceProvider.GetRequiredService<MasterContext>();
                await context.Database.ExecuteSqlRawAsync(unitedScript);
            }
            _logger.LogTrace("Скрипт выполнен");
        }

        private async Task<string> CreateScript<TEntity>(ICollection<TEntity> entities) where TEntity : IEntityDB
        {
            string key = typeof(TEntity).GetKey();
            _logger.LogTrace("Получение скриптов для {key} у Person в количестве:{count}", key, entities.Count);
            StringBuilder script = new StringBuilder();
            foreach (TEntity chm in entities)
            {
                _logger.LogTrace("{key} - ID:{id}", key, chm.ID);
                var denormalizer = serviceProvider.GetRequiredKeyedService<IEntityDenormalizer>(typeof(TEntity).GetKey());
                string receivedScript = await denormalizer.Append(chm);
                _logger.LogTrace("Получен скрипт:\n{script}", receivedScript);
                script.Append(receivedScript);
            }
            return script.ToString();
        }
    }
}
