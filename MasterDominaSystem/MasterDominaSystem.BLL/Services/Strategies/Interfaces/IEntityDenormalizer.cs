using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace MasterDominaSystem.BLL.Services.Strategies.Interfaces
{
    /// <summary>
    /// Генерирует скрипты SQL для применения в базу данных
    /// </summary>
    public interface IEntityDenormalizer<in TEntity> : IEntityDenormalizer
        where TEntity : IEntityDB
    {
        string Append(TEntity entity);
        string IEntityDenormalizer.Append(IEntityDB entity) => Append(entity);

        string Delete(TEntity entity);
        string IEntityDenormalizer.Delete(IEntityDB entity) => Delete(entity);

        string Update(TEntity entity);
        string IEntityDenormalizer.Update(IEntityDB entity) => Update(entity);
    }

    /// <summary>
    /// Генерирует скрипты SQL для применения в базу данных
    /// </summary>
    public interface IEntityDenormalizer
    {
        string Append(IEntityDB entity);
        string Delete(IEntityDB entity);
        string Update(IEntityDB entity);
    }
}
