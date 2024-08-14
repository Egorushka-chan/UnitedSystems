using Grpc.Net.Client;
using Grpc.Net.ClientFactory;

using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.GRPC.Models;
using MasterDominaSystem.GRPC.Services.Extensions;
using MasterDominaSystem.GRPC.Services.Interfaces;

using Microsoft.Extensions.Options;

using WOSenderDB;

namespace MasterDominaSystem.GRPC.Services.Implementations
{
    public class GRPCDatabaseDownloader(IOptions<ConnectionGRPCSettings> options, IDatabaseDenormalizer denormalizer) : IDatabaseDownloader
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

            var clothesDB = from clothProto in response.Cloths
                            select clothProto.ConvertToDB();
            var personsDB = from personProto in response.Persons
                            select personProto.ConvertToDB();
            var physiquesDB = from physiqueProto in response.Physiques
                              select physiqueProto.ConvertToDB();
            var setsDB = from setsProto in response.Sets
                         select setsProto.ConvertToDB();
            var seasonsDB = from seasonProto in response.Seasons
                            select seasonProto.ConvertToDB();
            var setHasClothesDb = from setHasClothesProto in response.SetHasClothes
                                  select setHasClothesProto.ConvertToDB();
            var photosDB = from photoProto in response.Photos
                           select photoProto.ConvertToDB();
            var materialsDB = from materialProto in response.Materials
                              select materialProto.ConvertToDB();
            var clothMaterialsDB = from clothHasMaterialsProto in response.ClothHasMaterials
                                   select clothHasMaterialsProto.ConvertToDB();

            await denormalizer.AppendRange(personsDB);
            await denormalizer.AppendRange(physiquesDB);
            await denormalizer.AppendRange(setsDB);
            await denormalizer.AppendRange(seasonsDB);
            await denormalizer.AppendRange(setHasClothesDb);
            await denormalizer.AppendRange(clothesDB);
            await denormalizer.AppendRange(photosDB);
            await denormalizer.AppendRange(clothMaterialsDB);
            await denormalizer.AppendRange(materialsDB);
        }
    }
}
