using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class SetHasClothesDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default) 
        : GeneralEntityDenormalizer<SetHasClothes>(options, environment)
    {
        public override string Append(SetHasClothes entity)
        {
            throw new NotImplementedException();
        }

        public override string Delete(SetHasClothes entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(SetHasClothes entity)
        {
            throw new NotImplementedException();
        }
    }
}
