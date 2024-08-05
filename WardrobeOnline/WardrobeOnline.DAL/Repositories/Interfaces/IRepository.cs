using Microsoft.EntityFrameworkCore;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace WardrobeOnline.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Это классный паттерн, если у тебя с базой происходят только какие то элементарные процессы.
    /// Однако мне в этом проекте понадобилось усложнить получение и применение данных, из-за чего он стал меня сильно ограничивать.
    /// Интерфейс КРУДа реализован в бизнес логике, не здесь
    /// </summary>
    [Obsolete]
    public interface IRepository<T> where T : class, IEntity
    {
        public IReadOnlyCollection<T> GetAll();
        public T? TryGet(int id);
        public DbSet<T> Filter();
        public Task<bool> TryAdd(T entity);
        public Task<bool> TryRemove(int id);
        public Task<bool> TryUpdate(T entity);
    }
}
