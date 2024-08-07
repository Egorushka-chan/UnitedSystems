using Grpc.Core;

using Microsoft.EntityFrameworkCore;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using WardrobeOnline.DAL.Interfaces;
using WardrobeOnline.GRPC.Services.Extensions;
using WardrobeOnline.GRPC.Services.Interfaces;

using WOSenderDB;

namespace WardrobeOnline.GRPC.Services.Implementations
{
    public class GRPCDatabaseUploader(IWardrobeContext _dbContext) : WODownloader.WODownloaderBase, IDatabaseUploader
    {
        private readonly int _packageSize = 500;
        public override async Task DownloadDatabaseEntities(RequestDownload request, IServerStreamWriter<ResponseDownload> responseStream, ServerCallContext context)
        {
            if (!request.Proceed) {
                return;
            }

            CancellationToken token = context.CancellationToken;

            int iteration = 1;
            Dictionary<string, bool> completed = new() {
                {nameof(Person), false },
                {nameof(Physique), false},
                {nameof(Set), false },
                {nameof(Season), false },
                {nameof(Cloth), false },
                {nameof(SetHasClothes), false },
                {nameof(Photo), false },
                {nameof(Material), false },
                {nameof(ClothHasMaterials), false }
            };

            while (!token.IsCancellationRequested) {
                ResponseDownload response = new() {
                    PackageNumber = iteration,
                    PackageSize = _packageSize
                };

                if (!completed[nameof(Cloth)]) {
                    var clothes = (await _dbContext.Clothes
                        .Skip(_packageSize * (iteration - 1))
                        .Take(_packageSize)
                        .ToListAsync(token))
                        .Select(element => element.ConvertToProto());

                    if (clothes.Any())
                        response.Cloths.AddRange(clothes);
                    else
                        completed[nameof(Cloth)] = true;
                }
                

                var persons = (await _dbContext.Persons
                    .Skip(_packageSize * (iteration - 1))
                    .Take(_packageSize)
                    .ToListAsync(token))
                    .Select(element => element.ConvertToProto());


                await responseStream.WriteAsync(response, token);

                iteration++;
            }
        }
    }
}
