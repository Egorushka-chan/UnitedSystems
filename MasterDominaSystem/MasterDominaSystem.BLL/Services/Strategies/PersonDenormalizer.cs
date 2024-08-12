using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class PersonDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default) 
        : GeneralEntityDenormalizer<Person>(options, environment)
    {
        public override string Append(Person entity)
        {
            throw new NotImplementedException();
        }

        public override string Delete(Person entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(Person entity)
        {
            throw new NotImplementedException();
        }
    }
}
