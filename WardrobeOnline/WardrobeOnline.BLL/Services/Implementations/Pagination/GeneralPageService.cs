using Microsoft.EntityFrameworkCore;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.BLL.Services.Implementations.Pagination
{
    public class GeneralPageService<TEntity> : IPaginationService<TEntity>
        where TEntity : class, IEntityDB
    {
        protected IWardrobeContext _context;
        public GeneralPageService(IWardrobeContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetPagedQuantityOf(int pageIndex, int pageSize)
        {
            int totalCount = await GetTotalSize();
            int maxPage = totalCount / pageSize;
            if (totalCount % pageSize != 0)
                maxPage++;
            if (maxPage < pageIndex) // проверка что страница не слишком большая
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex), totalCount, "Such page cannot be created");
            }

            var entities = await GetEntities(pageIndex, pageSize);

            return entities;
        }

        protected virtual Task<List<TEntity>> GetEntities(int pageIndex, int pageSize)
        {
            return _context.DBSet<TEntity>()
                .Skip((pageIndex-1) * pageSize)
                .Take(pageSize)
                .OrderBy(d => d.ID)
                .ToListAsync();
        }

        protected virtual Task<int> GetTotalSize()
        {
            return _context.DBSet<TEntity>().CountAsync();
        }
    }
}
