using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class ClothHasMaterialsDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default) 
        : GeneralEntityDenormalizer<ClothHasMaterials>(options, environment)
    {
        protected override string[] DefaultAllowedReports { get; set; } = [
            typeof(ReportCloth).GetKey(),
            typeof(ReportPerson).GetKey()
        ];

        protected override string FormatAppend(string script, ClothHasMaterials entity)
        {
            return string.Format(script, entity.ID, entity.ClothID, entity.MaterialID);
        }

        protected override string FormatDelete(string script, ClothHasMaterials entity)
        {
            return string.Format(script, entity.ID, entity.ClothID, entity.MaterialID);
        }
    }
}
