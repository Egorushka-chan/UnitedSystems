using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class MaterialDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default) 
        : GeneralEntityDenormalizer<Material>(options, environment)
    {
        public override string Append(Material entity)
        {
            throw new NotImplementedException();
        }

        public override string Delete(Material entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(Material entity)
        {
            throw new NotImplementedException();
        }
    }
}
