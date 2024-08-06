using Grpc.Net.Client;

using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.GRPC.Models;
using MasterDominaSystem.GRPC.Services.Interfaces;

using Microsoft.Extensions.Options;

using UnitedSystems.CommonLibrary.ManyEntitiesSender.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities;

using WODownloaderClient;

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

                
            }
        }


    }
}
