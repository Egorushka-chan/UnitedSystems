using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace MasterDominaSystem.BLL.Services.Strategies.Interfaces
{
    /// <summary>
    /// Генерирует скрипты SQL для применения в базу данных
    /// </summary>
    public interface IEntityDenormalizer<in TEntity> : IEntityDenormalizer
        where TEntity : IEntity
    {
        string Append(TEntity entity);
        string IEntityDenormalizer.Append(IEntity entity) => Append(entity);

        string Delete(TEntity entity);
        string IEntityDenormalizer.Delete(IEntity entity) => Delete(entity);

        string Update(TEntity entity);
        string IEntityDenormalizer.Update(IEntity entity) => Update(entity);
    }

    /// <summary>
    /// Генерирует скрипты SQL для применения в базу данных
    /// </summary>
    public interface IEntityDenormalizer
    {
        string Append(IEntity entity);
        string Delete(IEntity entity);
        string Update(IEntity entity);
    }
}
