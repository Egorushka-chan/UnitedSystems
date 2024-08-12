using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class ClothDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default) 
        : GeneralEntityDenormalizer<Cloth>(options, environment)
    {
        public override string Append(Cloth entity)
        {
            throw new NotImplementedException();
        }

        public override string Delete(Cloth entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(Cloth entity)
        {
            throw new NotImplementedException();
        }
    }
}
