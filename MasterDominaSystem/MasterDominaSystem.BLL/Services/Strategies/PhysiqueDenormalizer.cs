using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class PhysiqueDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default) 
        : GeneralEntityDenormalizer<Physique>(options, environment)
    {
        public override string Append(Physique entity)
        {
            throw new NotImplementedException();
        }

        public override string Delete(Physique entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(Physique entity)
        {
            throw new NotImplementedException();
        }
    }
}
