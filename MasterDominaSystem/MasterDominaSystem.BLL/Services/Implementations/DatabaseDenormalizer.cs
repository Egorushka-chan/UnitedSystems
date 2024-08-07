using MasterDominaSystem.BLL.Services.Abstractions;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace MasterDominaSystem.BLL.Services.Implementations
{
    public class DatabaseDenormalizer(IServiceProvider services) : IDatabaseDenormalizer
    {
        public Task AppendNew(IEntityDB entity)
        {
            throw new NotImplementedException();
        }

        public Task AppendNew(IEnumerable<IEntityDB> entities)
        {
            throw new NotImplementedException();
        }
    }
}
