using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class ClothHasMaterialsDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default) 
        : GeneralEntityDenormalizer<ClothHasMaterials>(options, environment)
    {
        public override string Append(ClothHasMaterials entity)
        {
            throw new NotImplementedException();
        }

        public override string Delete(ClothHasMaterials entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(ClothHasMaterials entity)
        {
            throw new NotImplementedException();
        }
    }
}
