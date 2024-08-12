using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace MasterDominaSystem.BLL.Services.Abstractions
{
    public interface IDatabaseDenormalizer
    {
        Task Append<TEntity>(TEntity entity) where TEntity : IEntityDB;

        Task AppendRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : IEntityDB;
    }
}
