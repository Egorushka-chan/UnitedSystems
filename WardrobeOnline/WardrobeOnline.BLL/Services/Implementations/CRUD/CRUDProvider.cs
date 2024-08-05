using Microsoft.EntityFrameworkCore;

using UnitedSystems.CommonLibrary.WardrobeOnline.DTO.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.BLL.Services.Implementations.CRUD
{
    public abstract class CRUDProvider<TEntityDTO, TEntityDB>
        : ICRUDProvider<TEntityDTO>
        where TEntityDTO : class, IEntityDTO
        where TEntityDB : class, IEntity
    {
        protected readonly IWardrobeContext _context;
        protected readonly IPaginationService<TEntityDB> _pagination;
        protected readonly ICastHelper _castHelper;
        protected readonly IImageProvider _imageProvider;
        protected readonly IEventBus _eventBus;

        public CRUDProvider(IWardrobeContext context, IPaginationService<TEntityDB> pagination,
            ICastHelper castHelper, IImageProvider imageProvider, IEventBus eventBus)
        {
            _context = context;
            _pagination = pagination;
            _castHelper = castHelper;
            _imageProvider = imageProvider;
            _eventBus = eventBus;
        }

        public async Task<IReadOnlyList<TEntityDTO>> GetPagedQuantity(int pageIndex, int pageSize)
        {
            var list = await _pagination.GetPagedQuantityOf(pageIndex, pageSize);

            List<TEntityDTO> resultList = [];
            foreach (var item in list)
            {
                TEntityDTO itemDTO = await GetTranslateToDTO(item) ?? throw new InvalidOperationException($"NULL! {nameof(itemDTO)}");
                resultList.Add(itemDTO);
            }
            return resultList;
        }

        public async Task<TEntityDTO?> TryAddAsync(TEntityDTO entity)
        {
            TEntityDB? entityDB = await AddTranslateToDB(entity);
            if (entityDB == null)
                return null;

            _context.DBSet<TEntityDB>().Add(entityDB);
            int result = await SaveChangesAsync();

            await _eventBus.PublishAsync(new WOCreatedCRUDEvent<TEntityDB>() {
                Entities = [
                    entityDB
                ]
            });

            return await AddTranslateToDTO(entityDB);
        }

        public async Task<TEntityDTO?> TryGetAsync(int id)
        {
            var result = await GetFromDBbyID(id);
            if (result == null)
                return null;
            return await GetTranslateToDTO(result);
        }

        public async Task<bool> TryRemoveAsync(int id)
        {
            int deleted = await _context.DBSet<TEntityDB>().Where(ent => ent.ID == id).ExecuteDeleteAsync();

            bool successful = deleted > 0;

            if (successful) {
                await _eventBus.PublishAsync(new WODeletedCRUDEvent<TEntityDB>() {
                    EntitiesIDs = [id]
                });
            }

            return successful;
        }

        public async Task<TEntityDTO?> TryUpdateAsync(TEntityDTO entity)
        {
            TEntityDB? entityDB = await UpdateTranslateToDB(entity);
            if (entityDB == null)
                return null;
            var saveTask = SaveChangesAsync();

            var eventTask = _eventBus.PublishAsync(new WOUpdatedCRUDEvent<TEntityDB>() {
                Entities = [entityDB]
            });

            var translateTask = UpdateTranslateToDTO(entityDB);

            await Task.WhenAll(saveTask, eventTask, translateTask); 
            return translateTask.Result;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected abstract Task<TEntityDB?> GetFromDBbyID(int id);
        protected abstract Task<TEntityDTO?> GetTranslateToDTO(TEntityDB entityDB);

        protected abstract Task<TEntityDB?> AddTranslateToDB(TEntityDTO entityDTO);

        protected abstract Task<TEntityDTO?> AddTranslateToDTO(TEntityDB entityDB);

        protected abstract Task<TEntityDB?> UpdateTranslateToDB(TEntityDTO entityDTO);

        protected abstract Task<TEntityDTO?> UpdateTranslateToDTO(TEntityDB entityDB);
    }
}
