using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace MasterDominaSystem.BLL.Services.Abstractions
{
    public interface IDatabaseDenormalizer
    {
        Task AppendNew(IEntity entity);

        Task AppendNew(IEnumerable<IEntity> entities);
    }
}
