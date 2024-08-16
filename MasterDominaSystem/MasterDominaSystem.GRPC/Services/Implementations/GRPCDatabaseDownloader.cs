using System.Diagnostics.Eventing.Reader;
using System.Runtime.ExceptionServices;

using Grpc.Net.Client;

using MasterDominaSystem.GRPC.Models;
using MasterDominaSystem.GRPC.Services.Interfaces;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

using WOSenderDB;

namespace MasterDominaSystem.GRPC.Services.Implementations
{
    public class GRPCDatabaseDownloader(IOptions<ConnectionGRPCSettings> options, IServiceProvider serviceProvider) : IDatabaseDownloader
    {
        readonly ConnectionGRPCSettings _options = options.Value;
        public async Task DownloadDataAsync(CancellationToken cancellationToken = default)
        {
            using var channel = GrpcChannel.ForAddress(_options.ConnectionString);
            var client = new WODownloader.WODownloaderClient(channel);

            RequestDownload request = new()
            {
                Proceed = true
            };

            using var responseStreamMeta = client.DownloadDatabaseEntities(request, cancellationToken: cancellationToken);
            var responseStream = responseStreamMeta.ResponseStream;
            
            while (await responseStream.MoveNext(cancellationToken))
            {
                ResponseDownload response = responseStream.Current;
                await ProcessMessage(response);
            }
        }

        private async Task ProcessMessage(ResponseDownload response)
        {
            await HandleClothes(response);
            await HandlePersons(response);
            await HandleSets(response);
        }

        private async Task HandleClothes(ResponseDownload response)
        {
            foreach (var clothIE in response.ClothIEs) {
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

                var handlers = serviceProvider.GetKeyedServices<IIntegrationEventHandler>(integration);
                foreach (var handler in handlers) {
                    await handler.Handle(integration);
                }
            }
        }

        private async Task HandlePersons(ResponseDownload response)
        {
            foreach (var personIE in response.PersonIEs) {
                PersonWrapProto personWrap = new(personIE.Person);
                Physique[] physiques =
                    (from physiqueProto in personIE.Physiques
                     select (Physique)new PhysiqueWrapProto(physiqueProto))
                    .ToArray();

                Person person = personWrap;

                var integration = new AppendedPersonIntegrationEvent(person) {
                    Physiques = physiques
                };

                var handlers = serviceProvider.GetKeyedServices<IIntegrationEventHandler>(integration);
                foreach (var handler in handlers) {
                    await handler.Handle(integration);
                }
            }
        }

        private async Task HandleSets(ResponseDownload response)
        {
            foreach(var setIE in response.SetIEs) {
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

                var handlers = serviceProvider.GetKeyedServices<IIntegrationEventHandler>(integration);
                foreach(var handler in handlers) {
                    await handler.Handle(integration);
                }
            }
        }
    }
}
