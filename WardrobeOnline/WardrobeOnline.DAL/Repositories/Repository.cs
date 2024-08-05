using Microsoft.EntityFrameworkCore;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WardrobeOnline.DAL.Interfaces;
using WardrobeOnline.DAL.Repositories.Interfaces;

namespace WardrobeOnline.DAL.Repositories
{
    /// <summary>
    /// Это классный паттерн, если у тебя с базой происходят только какие то элементарные процессы.
    /// Однако мне в этом проекте понадобилось усложнить получение и применение данных, из-за чего он стал меня сильно ограничивать
    /// </summary>
    [Obsolete]
    public class Repository<T>(IWardrobeContext wardrobeContext) : IRepository<T> where T : class, IEntity
    {
        private readonly IWardrobeContext _wardrobeContext = wardrobeContext ?? throw new ArgumentNullException(nameof(wardrobeContext));

        public IReadOnlyCollection<T> GetAll()
        {
            return _wardrobeContext.DBSet<T>().ToList().AsReadOnly();
        }
        public T? TryGet(int id)
        {           
            return _wardrobeContext.DBSet<T>().Find(id);
        }
        public DbSet<T> Filter()
        {
            return _wardrobeContext.DBSet<T>();
        }

        public async Task<bool> TryAdd(T entity)
        {
            var added = _wardrobeContext.DBSet<T>().Add(entity);
            //int createdID = added.Entity.ID;

            var changes = await _wardrobeContext.SaveChangesAsync();
            return changes == 1;
        }

        public async Task<bool> TryUpdate(T entity)
        {
            _wardrobeContext.DBSet<T>().Update(entity);
            var changes = await _wardrobeContext.SaveChangesAsync();
            return changes == 1;
        }

        public async Task<bool> TryRemove(int id)
        {
            T entity = _wardrobeContext.DBSet<T>().Where(el => el.ID == id).First();
            _wardrobeContext.DBSet<T>().Remove(entity);
            var changes = await _wardrobeContext.SaveChangesAsync();
            return changes == 1;
        }
    }
}
