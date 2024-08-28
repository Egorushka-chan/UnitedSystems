using Grpc.Core;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

using WardrobeOnline.DAL.Interfaces;
using WardrobeOnline.GRPC.Services.Interfaces;

using WOSenderDB;

namespace WardrobeOnline.GRPC.Services.Implementations
{
    public class GRPCDatabaseUploader : WODownloader.WODownloaderBase, IDatabaseUploader
    {
        private readonly IWardrobeContext _dbContext;
        private readonly ILogger<GRPCDatabaseUploader> _logger;
        public GRPCDatabaseUploader(IWardrobeContext dbContext, ILogger<GRPCDatabaseUploader> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        private readonly int _packageSize = 100;
        public override async Task DownloadDatabaseEntities(RequestDownload request, IServerStreamWriter<ResponseDownload> responseStream, ServerCallContext context)
        {
            if (!request.Proceed) {
                return;
            }

            _logger.LogInformation("Начало выполнения запроса DownloadDatabaseEntities");

            CancellationToken token = context.CancellationToken;
            _logger.LogInformation("Токен отмены: {token}", token.IsCancellationRequested ? "True" : "False");

            await SendClothes(responseStream, token);
            await SendPersons(responseStream, token);
            await SendSets(responseStream, token);
            context.Status = Status.DefaultSuccess;
        }

        private async Task SendClothes(IServerStreamWriter<ResponseDownload> responseStream, CancellationToken token)
        {
            _logger.LogInformation("Начало отправки одежды");
            int iteration = 1;
            bool isEnd = false;
            while (!token.IsCancellationRequested && !isEnd) {
                _logger.LogInformation("Отправка одежды - итерация {iteration}", iteration);
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
                   .Where(p => clothIDs.Contains(p.ClothID))
                   .ToListAsync(token);

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
                        if(photo.ClothID == cloth.ID) {
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

                if(!isEnd)
                {
                    await responseStream.WriteAsync(response, token);
                    _logger.LogInformation("Отправка одежды - итерация {iteration} отправлена, кол-во элементов - {count}", iteration, response.ClothIEs.Count);
                }
                else
                {
                    _logger.LogInformation("Пакет пустой - {status}", isEnd ? "True" : "False");
                }

                iteration++;
            }

            _logger.LogInformation("Вся одежда отправлена");
        }

        private async Task SendPersons(IServerStreamWriter<ResponseDownload> responseStream, CancellationToken token)
        {
            _logger.LogInformation("Начало отправки персон");
            int iteration = 1;
            bool isEnd = false;
            while (!token.IsCancellationRequested && !isEnd) {
                _logger.LogInformation("Отправка персон - итерация {iteration}", iteration);
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

                foreach (Person person in persons) {
                    PersonIntegrationEventProto IE = new() {
                        Person = person
                    };
                    foreach(Physique physique in person.Physiques) {
                        IE.Physiques.Add(physique);
                    }

                    response.PersonIEs.Add(IE);
                }

                if(!isEnd)
                {
                    await responseStream.WriteAsync(response, token);
                    _logger.LogInformation("Отправка персон - итерация {iteration} отправлена, кол-во элементов - {count}", iteration, response.PersonIEs.Count);

                }
                else
                {
                    _logger.LogInformation("Пакет пустой - {status}", isEnd ? "True" : "False");
                }

                iteration++;
            }

            _logger.LogInformation("Все персоны отправлены");
        }

        private async Task SendSets(IServerStreamWriter<ResponseDownload> responseStream, CancellationToken token)
        {
            _logger.LogInformation("Начало отправки наборов");
            int iteration = 1;
            bool isEnd = false;
            while (!token.IsCancellationRequested && !isEnd) {
                _logger.LogInformation("Отправка наборов - итерация {iteration}", iteration);

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

                if(!isEnd)
                {
                    await responseStream.WriteAsync(response, token);
                    _logger.LogInformation("Отправка наборов - итерация {iteration} отправлена, кол-во элементов - {count}", iteration, response.SetIEs.Count);
                }
                else
                {
                    _logger.LogInformation("Пакет пустой - {status}", isEnd ? "True" : "False");
                }

                iteration++;
            }
            _logger.LogInformation("Все наборы отправлены");
        }

    }
}
