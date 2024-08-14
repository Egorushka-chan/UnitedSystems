using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class SetHasClothesDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default) 
        : GeneralEntityDenormalizer<SetHasClothes>(options, environment)
    {
        protected override string[] DefaultAllowedReports { get; set; } = [
            typeof(ReportPerson).GetKey()
        ];

        protected override string FormatAppend(string script, SetHasClothes entity)
        {
            return string.Format(script, entity.ID, entity.ClothID, entity.SetID);
        }

        protected override string FormatDelete(string script, SetHasClothes entity)
        {
            return string.Format(script, entity.ID, entity.ClothID, entity.SetID);
        }
    }
}
