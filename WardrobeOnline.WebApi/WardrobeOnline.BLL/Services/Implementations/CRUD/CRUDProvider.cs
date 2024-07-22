using System.Collections.Immutable;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

using WardrobeOnline.BLL.Models;
using WardrobeOnline.BLL.Models.Interfaces;
using WardrobeOnline.BLL.Services.Extensions;
using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Entities;
using WardrobeOnline.DAL.Interfaces;
using WardrobeOnline.DAL.Repositories.Interfaces;

namespace WardrobeOnline.BLL.Services.Implementations.CRUD
{
    public abstract class CRUDProvider<TEntityDTO, TEntityDB>
        : ICRUDProvider<TEntityDTO>
        where TEntityDTO : class, IEntityDTO
        where TEntityDB : class, IEntity
    {
        protected IWardrobeContext _context;
        protected IPaginationService<TEntityDB> _pagination;
        protected ICastHelper _castHelper;
        protected IImageProvider _imageProvider;

        public CRUDProvider(IWardrobeContext context, IPaginationService<TEntityDB> pagination, ICastHelper castHelper, IImageProvider imageProvider)
        {
            _context = context;
            _pagination = pagination;
            _castHelper = castHelper;
            _imageProvider = imageProvider;
        }

        public async Task<IReadOnlyList<TEntityDTO>> GetPagedQuantity(int pageIndex, int pageSize)
        {
            var list = await _pagination.GetPagedQuantityOf(pageIndex, pageSize);

            List<TEntityDTO> resultList = [];
            foreach (var item in list)
            {
                var itemDTO = await GetTranslateToDTO(item);
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
            return deleted > 0;
        }

        public async Task<TEntityDTO?> TryUpdateAsync(TEntityDTO entity)
        {
            TEntityDB? entityDB = await UpdateTranslateToDB(entity);
            if (entityDB == null)
                return null;
            await SaveChangesAsync();
            return await UpdateTranslateToDTO(entityDB);
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
