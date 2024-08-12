using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class SeasonDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default) 
        : GeneralEntityDenormalizer<Season>(options, environment)
    {
        public override string Append(Season entity)
        {
            throw new NotImplementedException();
        }

        public override string Delete(Season entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(Season entity)
        {
            throw new NotImplementedException();
        }
    }
}
