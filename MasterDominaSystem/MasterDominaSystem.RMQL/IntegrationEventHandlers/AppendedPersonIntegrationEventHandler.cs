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
    internal class AppendedPersonIntegrationEventHandler(IServiceProvider serviceProvider) :
        IIntegrationEventHandler<AppendedPersonIntegrationEvent>
    {
        private readonly ILogger<AppendedPersonIntegrationEventHandler> _logger = serviceProvider.GetRequiredService<ILogger<AppendedPersonIntegrationEventHandler>>();

        public async Task Handle(AppendedPersonIntegrationEvent @event)
        {
            _logger.LogDebug("Начало выполнения метода Handle обработчика событий AppendedPersonIntegrationEventHandler. " +
                "Событие: {id} {timestamp}",@event.ID, @event.TimeStamp);

            _logger.LogDebug("Получение скрипта для Person - ID:{id}, Name:{name}",@event.Person.ID, @event.Person.Name);
            var personDenormalizer = (IEntityDenormalizer<Person>)serviceProvider.GetRequiredKeyedService<IEntityDenormalizer>(typeof(Person).GetKey());
            string personScript = await personDenormalizer.Append(@event.Person);
            _logger.LogDebug("Получен скрипт:\n{script}", personScript);

            string physiquesScript = await CreateScript(@event.Physiques);

            string unitedScript = personScript + physiquesScript;
            _logger.LogDebug("Кол-во символов итогового скрипта: {length}", unitedScript.Length);

            using(var scope = serviceProvider.CreateScope())
            {
                MasterContext context = scope.ServiceProvider.GetRequiredService<MasterContext>();
                await context.Database.ExecuteSqlRawAsync(unitedScript);
            }
            _logger.LogDebug("Скрипт выполнен");
        }

        private async Task<string> CreateScript<TEntity>(ICollection<TEntity> entities) where TEntity : IEntityDB
        {
            string key = typeof(TEntity).GetKey();
            _logger.LogDebug("Получение скриптов для {key} у Person в количестве:{count}", key, entities.Count);
            StringBuilder script = new StringBuilder();
            foreach (TEntity chm in entities)
            {
                _logger.LogDebug("{key} - ID:{id}", key, chm.ID);
                var denormalizer = serviceProvider.GetRequiredKeyedService<IEntityDenormalizer>(typeof(TEntity).GetKey());
                string receivedScript = await denormalizer.Append(chm);
                _logger.LogDebug("Получен скрипт:\n{script}", receivedScript);
                script.Append(receivedScript);
            }
            return script.ToString();
        }
    }
}
