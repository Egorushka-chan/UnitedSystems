using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace MasterDominaSystem.BLL.Services.Strategies.Interfaces
{
    /// <inheritdoc cref="IEntityDenormalizer"/>
    public interface IEntityDenormalizer<in TEntity> : IEntityDenormalizer
        where TEntity : IEntityDB
    {
        Task<string> Append(TEntity entity, Type? report = default);
        Task<string> IEntityDenormalizer.Append(IEntityDB entity, Type? report = default) => Append((TEntity)entity, report);

        Task<string> Delete(TEntity entity, Type? report = default);
        Task<string> IEntityDenormalizer.Delete(IEntityDB entity, Type? report = default) => Delete((TEntity)entity, report);
    }

    /// <summary>
    /// Генерирует скрипты SQL для применения в базу данных
    /// </summary>
    public interface IEntityDenormalizer
    {
        Task<string> Append(IEntityDB entity, Type? report = default);
        Task<string> Delete(IEntityDB entity, Type? report = default);
    }
}
