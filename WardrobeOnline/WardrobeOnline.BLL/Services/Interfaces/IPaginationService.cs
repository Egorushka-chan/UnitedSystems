using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.BLL.Services.Interfaces
{
    public interface IPaginationService<TEntity>
        where TEntity : class, IEntity
    {
        public Task<List<TEntity>> GetPagedQuantityOf(int pageIndex, int pageSize);
    }
}
