using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class PhotoDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default) 
        : GeneralEntityDenormalizer<Photo>(options, environment)
    {
        public override string Append(Photo entity)
        {
            throw new NotImplementedException();
        }

        public override string Delete(Photo entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(Photo entity)
        {
            throw new NotImplementedException();
        }
    }
}
