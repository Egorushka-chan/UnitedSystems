using Grpc.Core;

using Microsoft.EntityFrameworkCore;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

using WardrobeOnline.DAL.Interfaces;
using WardrobeOnline.GRPC.Services.Interfaces;

using WOSenderDB;

namespace WardrobeOnline.GRPC.Services.Implementations
{
    public class GRPCDatabaseUploader : WODownloader.WODownloaderBase, IDatabaseUploader
    {
        private readonly IWardrobeContext _dbContext;
        public GRPCDatabaseUploader(IWardrobeContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly int _packageSize = 100;
        public override async Task DownloadDatabaseEntities(RequestDownload request, IServerStreamWriter<ResponseDownload> responseStream, ServerCallContext context)
        {
            if (!request.Proceed) {
                return;
            }

            CancellationToken token = context.CancellationToken;

            await SendClothes(responseStream, token);
            await SendPersons(responseStream, token);
            await SendSets(responseStream, token);
        }

        private async Task SendClothes(IServerStreamWriter<ResponseDownload> responseStream, CancellationToken token)
        {
            int iteration = 1;
            bool isEnd = false;
            while (!token.IsCancellationRequested && !isEnd) {
                ResponseDownload response = new() {
                    PackageNumber = iteration,
                    PackageSize = _packageSize
                };

                var clothes = (await _dbContext.Clothes
                    .Skip(_packageSize * (iteration - 1))
                    .Take(_packageSize)
                    .Include(c => c.ClothHasMaterials)
                    .ThenInclude(chm => chm.Material)
                    .ToListAsync(token));

                if (clothes.Count == 0)
                    isEnd = true;

                var clothIDs = from cloth in clothes
                               select cloth.ID;

                var photos = await _dbContext.Photos
                   .Where(p => clothIDs.Contains(p.ID))
                   .ToListAsync(token);


                List<ClothIntegrationIventProto> integrationEvents = [];
                foreach (Cloth cloth in clothes) {
                    ClothProto clothProto = cloth;

                    List<ClothHasMaterialsProto> hasMaterialsProtos = [];
                    List<MaterialProto> materialProtos = [];
                    List<PhotoProto> photoProtos = [];

                    foreach (ClothHasMaterials hasMaterials in cloth.ClothHasMaterials) {
                        if(hasMaterials.Material != null) {
                            hasMaterialsProtos.Add(hasMaterials);
                            materialProtos.Add(hasMaterials.Material);
                        }
                    }

                    foreach (Photo photo in photos) {
                        if (photo.ID == cloth.ID) {
                            photoProtos.Add(photo);
                        }
                    }

                    ClothIntegrationIventProto IE = new() {
                        Cloth = clothProto,
                        MaterialIDs = { hasMaterialsProtos },
                        MaterialsValues = { materialProtos },
                        Photos = { photoProtos }
                    };

                    response.ClothIEs.Add(IE);
                }

                await responseStream.WriteAsync(response, token);

                iteration++;
            }
        }

        private async Task SendPersons(IServerStreamWriter<ResponseDownload> responseStream, CancellationToken token)
        {
            int iteration = 1;
            bool isEnd = false;
            while (!token.IsCancellationRequested && !isEnd) {
                ResponseDownload response = new() {
                    PackageNumber = iteration,
                    PackageSize = _packageSize
                };

                var persons = await _dbContext.Persons
                    .Skip(_packageSize * (iteration - 1))
                    .Take(_packageSize)
                    .Include(c => c.Physiques)
                    .ToListAsync(token);

                if (persons.Count == 0)
                    isEnd = true;

                List<PersonIntegrationEventProto> integrationEvents = [];
                foreach (Person person in persons) {
                    PersonIntegrationEventProto IE = new() {
                        Person = person
                    };
                    foreach(Physique physique in person.Physiques) {
                        IE.Physiques.Add(physique);
                    }

                    response.PersonIEs.Add(IE);
                }

                await responseStream.WriteAsync(response, token);

                iteration++;
            }
        }

        private async Task SendSets(IServerStreamWriter<ResponseDownload> responseStream, CancellationToken token)
        {
            int iteration = 1;
            bool isEnd = false;
            while (!token.IsCancellationRequested && !isEnd) {
                ResponseDownload response = new() {
                    PackageNumber = iteration,
                    PackageSize = _packageSize
                };

                var sets = await _dbContext.Sets
                    .Skip(_packageSize * (iteration - 1))
                    .Take(_packageSize)
                    .Include(s => s.Season)
                    .Include(s => s.SetHasClothes)
                    .ToListAsync(token);

                if (sets.Count == 0)
                    isEnd = true;

                List<PersonIntegrationEventProto> integrationEvents = [];
                foreach (Set set in sets) {
                    SetIntegrationEventProto IE = new() {
                        Set = set,
                        Season = set.Season ?? throw new InvalidOperationException($"Why relation to Set:id:{set.ID} Season is null???")
                    };
                    foreach (SetHasClothes hasClothes in set.SetHasClothes) {
                        IE.ClothIDs.Add(hasClothes);
                    }

                    response.SetIEs.Add(IE);
                }

                await responseStream.WriteAsync(response, token);

                iteration++;
            }

        }

    }
}
