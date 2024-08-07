using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace MasterDominaSystem.BLL.Services.Abstractions
{
    public interface IDatabaseDenormalizer
    {
        Task AppendNew(IEntityDB entity);

        Task AppendNew(IEnumerable<IEntityDB> entities);
    }
}
