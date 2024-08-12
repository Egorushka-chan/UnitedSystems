using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class SetDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default)
        : GeneralEntityDenormalizer<Set>(options, environment)
    {
        public override string Append(Set entity)
        {
            throw new NotImplementedException();
        }

        public override string Delete(Set entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(Set entity)
        {
            throw new NotImplementedException();
        }
    }
}
