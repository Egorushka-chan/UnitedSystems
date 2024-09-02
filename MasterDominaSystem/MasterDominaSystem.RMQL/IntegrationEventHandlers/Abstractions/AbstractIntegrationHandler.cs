using System.Text;

using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL;
using MasterDominaSystem.RMQL.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using UnitedSystems.CommonLibrary.Messages;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.EventBus;
using UnitedSystems.EventBus.Interfaces;

namespace MasterDominaSystem.RMQL.IntegrationEventHandlers.Abstractions
{
    internal abstract class AbstractIntegrationHandler<TEvent>(
        ILogger logger,
        IServiceProvider serviceProvider
        ) : IIntegrationEventHandler<TEvent>
        where TEvent : IntegrationEvent
    {
        protected ILogger _logger = logger;
        protected readonly IServiceProvider _services = serviceProvider;

        private const int ErrorScriptRevealArea = 100;
        private const int ErrorScriptConcreteArea = 4;

        public async Task Handle(TEvent @event)
        {
            _logger.LogDebug("Начало выполнения метода Handle {ThisName}. " +
                "Событие: {id} {timestamp}", ThisName, @event.ID, @event.TimeStamp);

            string unitedScript = await GenerateScript(@event);

            _logger.LogDebug("Кол-во символов итогового скрипта: {length}", unitedScript.Length);

            using (var scope = _services.CreateScope())
            {
                MasterContext context = scope.ServiceProvider.GetRequiredService<MasterContext>();
                try
                {
                    await context.Database.ExecuteSqlRawAsync(unitedScript);
                }
                catch (FormatException format)
                {
                    int indexOffset = format.Message.IndexOf("offset");
                    bool isZeroBased = format.Message.Contains("(zero based)");
                    if (indexOffset != -1)
                    {
                        FormatException wrapperException = SpecifyOffsetException(unitedScript, format, indexOffset);
                        throw wrapperException;
                    }
                    else if (isZeroBased)
                    {
                        FormatException wrapperException = SpecifyZeroBasedException(unitedScript, format);
                        throw wrapperException;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            _logger.LogInformation("Скрипт выполнен");
        }

        private FormatException SpecifyZeroBasedException(string script, FormatException exception)
        {
            string message = "Обнаружено неопределённое значение:";
            foreach (int index in script.AllIndexesOf("{"))
            {
                if (script[index - 1] != '\'')
                {
                    int? closeBracketsIndex = null;
                    for (int i = index; i < script.Length; i++)
                    {
                        char letter = script[i];
                        if (letter == '}')
                        {
                            closeBracketsIndex = i;
                        }
                    }

                    if (closeBracketsIndex is null)
                    {
                        string area = script.GetArea(index, 20);
                        message += $"\nОтсутствие закрытия у фигурной скобки: offset:{index} - {area}";
                    }
                    else
                    {
                        message += $"\nОбласть:\n" +
                            $"{script.GetArea(index, 30)}\n" +
                            $"Значение: {script[index..(closeBracketsIndex.Value + 1)]}";
                    }
                }
            }
            return new(message, exception);
        }

        private static FormatException SpecifyOffsetException(string script, FormatException exception, int index)
        {
            bool numberStarted = false;
            string numberString = "";
            for (int i = index; i < exception.Message.Length; i++)
            {
                char letter = exception.Message[i];

                if (!numberStarted)
                {
                    if (char.IsDigit(letter))
                    {
                        numberString += letter;
                        numberStarted = true;
                    }
                }
                else
                {
                    if (!char.IsDigit(letter))
                        break;

                    numberString += letter;
                }
            }
            int number = int.Parse(numberString);

            string errorArea = script.GetArea(number, ErrorScriptRevealArea);
            string errorPlace = script.GetArea(number, ErrorScriptConcreteArea);

            string completeMessage = $"SQL не смог обработать формат запроса. Скорее всего, в запросе допущена ошибка.\n" +
                $"Область кода ошибки:\n" +
                $"{errorArea}\n" +
                $"Конкретное место: offset:{number} - {errorPlace}";
            FormatException wrapperException = new(completeMessage, exception);
            return wrapperException;
        }

        protected async Task<string> CreateScript<TEntity>(ICollection<TEntity> entities) where TEntity : IEntityDB
        {
            string name = typeof(TEntity).Name;
            _logger.LogInformation("Получение скриптов для {key} у Cloth в количестве:{count}", name, entities.Count);
            StringBuilder script = new();
            foreach (TEntity chm in entities)
            {
                _logger.LogInformation("{key} - ID:{id}", name, chm.ID);
                var denormalizer = _services.GetRequiredKeyedService<IEntityDenormalizer>(typeof(TEntity).GetKey());
                string receivedScript = await denormalizer.Append(chm);
                _logger.LogInformation("Получен скрипт:\n{script}", receivedScript);
                script.Append(receivedScript);
            }
            return script.ToString();
        }

        public abstract Task<string> GenerateScript(TEvent @event);
        public abstract string ThisName { get; }
    }
}
