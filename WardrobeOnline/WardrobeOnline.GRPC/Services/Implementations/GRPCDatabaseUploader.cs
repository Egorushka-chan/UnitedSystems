using Grpc.Core;

using Microsoft.EntityFrameworkCore;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WardrobeOnline.DAL.Interfaces;
using WardrobeOnline.GRPC.Services.Interfaces;

using WOSenderDB;

namespace WardrobeOnline.GRPC.Services.Implementations
{
    public class GRPCDatabaseUploader(IWardrobeContext _dbContext) : WODownloader.WODownloaderBase, IDatabaseUploader
    {
        private readonly int _packageSize = 100;
        public override async Task DownloadDatabaseEntities(RequestDownload request, IServerStreamWriter<ResponseDownload> responseStream, ServerCallContext context)
        {
            if (!request.Proceed) {
                return;
            }

            CancellationToken token = context.CancellationToken;

            await SendAsync<Person>(responseStream, token);
            await SendAsync<Physique>(responseStream, token);
            await SendAsync<Set>(responseStream, token);
            await SendAsync<Season>(responseStream, token);
            await SendAsync<Cloth>(responseStream, token);
            await SendAsync<SetHasClothes>(responseStream, token);
            await SendAsync<Photo>(responseStream, token);
            await SendAsync<Material>(responseStream, token);
            await SendAsync<ClothHasMaterials>(responseStream, token);
        }

        private async Task SendAsync<TEntity>(IServerStreamWriter<ResponseDownload> responseStream, CancellationToken token)
            where TEntity : EntityDB
        {
            int iteration = 1;
            bool isEnd = false;
            while (!token.IsCancellationRequested && !isEnd) {
                
                ResponseDownload response = new() {
                    PackageNumber = iteration,
                    PackageSize = _packageSize
                };
                
                var entitiesProto = (await _dbContext.DBSet<TEntity>()
                    .Skip(_packageSize * (iteration - 1))
                    .Take(_packageSize)
                    .ToListAsync(token))
                    .Select(element => ((EntityProto)element).Value);

                if (entitiesProto.Any())
                    foreach(var entityProto in entitiesProto) {
                        response.Cloths.Add(entityProto);
                    }
                else
                    isEnd = true;

                await responseStream.WriteAsync(response, token);

                iteration++;
            }
        }
    }
}
