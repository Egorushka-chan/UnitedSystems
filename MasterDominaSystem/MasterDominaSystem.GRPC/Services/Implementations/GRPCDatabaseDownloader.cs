using Grpc.Net.Client;

using MasterDominaSystem.GRPC.Models;
using MasterDominaSystem.GRPC.Services.Interfaces;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

using WOSenderDB;

namespace MasterDominaSystem.GRPC.Services.Implementations
{
    public class GRPCDatabaseDownloader(
        IOptions<ConnectionGRPCSettings> options,
        IServiceProvider serviceProvider,
        ILogger<GRPCDatabaseDownloader> logger
        ) : IDatabaseDownloader
    {
        readonly ConnectionGRPCSettings _options = options.Value;
        public async Task DownloadDataAsync(CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Поступила команда на загрузку базы WO через GRPC. Подключение: {connection}", _options.ConnectionString);
            using var channel = GrpcChannel.ForAddress(_options.ConnectionString);
            var client = new WODownloader.WODownloaderClient(channel);

            RequestDownload request = new()
            {
                Proceed = true
            };

            using var responseStreamMeta = client.DownloadDatabaseEntities(request, cancellationToken: cancellationToken);
            var responseStream = responseStreamMeta.ResponseStream;

            logger.LogInformation("Получен объект стрима");
            
            int iteration = 1;
            bool isEnd = false;
            bool hasAnything = await responseStream.MoveNext(cancellationToken);

            if (hasAnything)
            {
                while (!isEnd)
                {
                    ResponseDownload response = responseStream.Current;
                    logger.LogInformation("Обработка пакета №{myIterator}. Присланный пакет имеет номер {responseIterator}", iteration, response.PackageNumber);
                    bool hasContent = await ProcessMessage(response);
                    iteration++;
                    logger.LogInformation("Пакет №{myIterator} обработан", iteration);

                    if(!hasContent)
                        isEnd = true;
                    else
                    {
                        bool hasResponse = await responseStream.MoveNext(cancellationToken);
                        if(!hasResponse)
                            isEnd = true;
                    }
                }
            }
            else
            {
                logger.LogInformation("GRPC ничего не прислал");
            }
        }

        private async Task<bool> ProcessMessage(ResponseDownload response)
        {
            bool hasContent = false;
            if(response.ClothIEs.Count > 0){
                hasContent = true;
                await HandleClothes(response);
            }
            if(response.PersonIEs.Count > 0)
            {
                hasContent = true;
                await HandlePersons(response);
            }
            if(response.SetIEs.Count > 0)
            {
                hasContent = true;
                await HandleSets(response);
            }

            return hasContent;
        }

        private async Task HandleClothes(ResponseDownload response)
        {
            logger.LogDebug("Вход в метод обработки одежды");
            foreach (var clothIE in response.ClothIEs) {
                logger.LogInformation("Обработка интеграционного события ClothIE, Cloth.ID = {clothID}", clothIE.Cloth.ID);
                ClothWrapProto clothWrap = new(clothIE.Cloth);
                Material[] materials =
                    (from materialProto in clothIE.MaterialsValues
                     select (Material)new MaterialWrapProto(materialProto))
                    .ToArray();
                ClothHasMaterials[] clothMaterials =
                    (from clothMaterialID in clothIE.MaterialIDs
                     select (ClothHasMaterials)new ClothHasMaterialWrapProto(clothMaterialID))
                     .ToArray();

                Photo[] photos =
                    (from photoProto in clothIE.Photos
                     select (Photo)new PhotoWrapProto(photoProto))
                     .ToArray();

                Cloth cloth = clothWrap;

                var integration = new AppendedClothIntegrationEvent(cloth) {
                    Materials = materials,
                    ClothHasMaterials = clothMaterials,
                    Photos = photos
                };

                logger.LogInformation("Получение обработчиков для AppendedClothIntegrationEvent");
                var handlers = serviceProvider.GetKeyedServices<IIntegrationEventHandler>(typeof(AppendedClothIntegrationEvent));
                if (!handlers.Any())
                {
                    logger.LogWarning("Отсутствуют обработчики для AppendedClothIntegrationEvent");
                }
                int iterationHandler = 1;
                foreach (var handler in handlers) {
                    logger.LogInformation("Начало работы обработчика №{iteration} для AppendedClothIntegrationEvent", iterationHandler);
                    await handler.Handle(integration);
                    logger.LogInformation("Обработал обработчик №{iteration} для AppendedClothIntegrationEvent", iterationHandler);
                    iterationHandler++;
                }
            }
            logger.LogDebug("Метод обработки одежды завершился");
        }

        private async Task HandlePersons(ResponseDownload response)
        {
            logger.LogDebug("Вход в метод обработки персон");
            foreach (var personIE in response.PersonIEs) {
                logger.LogDebug("Обработка интеграционного события PersonIE, Person.ID = {personID}", personIE.Person.ID);
                PersonWrapProto personWrap = new(personIE.Person);
                Physique[] physiques =
                    (from physiqueProto in personIE.Physiques
                     select (Physique)new PhysiqueWrapProto(physiqueProto))
                    .ToArray();

                Person person = personWrap;

                var integration = new AppendedPersonIntegrationEvent(person) {
                    Physiques = physiques
                };

                var handlers = serviceProvider.GetKeyedServices<IIntegrationEventHandler>(typeof(AppendedPersonIntegrationEvent));
                if (!handlers.Any())
                {
                    logger.LogWarning("Отсутствуют обработчики для AppendedPersonIntegrationEvent");
                }
                int iterationHandler = 1;
                foreach (var handler in handlers) {
                    await handler.Handle(integration);
                    logger.LogDebug("Обработал обработчик №{iteration} для AppendedPersonIntegrationEvent", iterationHandler);
                    iterationHandler++;
                }
            }
            logger.LogDebug("Метод обработки персон завершился");
        }

        private async Task HandleSets(ResponseDownload response)
        {
            logger.LogDebug("Вход в метод обработки наборов");
            foreach(var setIE in response.SetIEs) {
                logger.LogInformation("Обработка интеграционного события SetIE, Set.ID = {setID}", setIE.Set.ID);
                Set set = new SetWrapProto(setIE.Set);
                Season season = new SeasonWrapProto(setIE.Season);
                set.Season = season;
                
                SetHasClothes[] setClothesIDs = 
                    (from proto in setIE.ClothIDs
                     select (SetHasClothes)new SetHasClothesWrapProto(proto))
                     .ToArray();
                set.SetHasClothes = setClothesIDs;

                var integration = new AppendedSetIntegrationEvent(set) {
                    Season = season,
                    SetHasClothes = setClothesIDs
                };

                var handlers = serviceProvider.GetKeyedServices<IIntegrationEventHandler>(typeof(AppendedSetIntegrationEvent));
                if (!handlers.Any())
                {
                    logger.LogWarning("Отсутствуют обработчики для AppendedSetIntegrationEvent");
                }

                int iterationHandler = 1;
                foreach(var handler in handlers) {
                    await handler.Handle(integration);
                    logger.LogDebug("Обработал обработчик №{iteration} для AppendedSetIntegrationEvent", iterationHandler);
                    iterationHandler++;
                }
            }
            logger.LogDebug("Метод обработки наборов завершился");
        }
    }
}
