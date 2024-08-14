using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class MaterialDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default) 
        : GeneralEntityDenormalizer<Material>(options, environment)
    {
        protected override string[] DefaultAllowedReports { get; set; } = [
            typeof(ReportCloth).GetKey()
        ];

        protected override string FormatAppend(string script, Material entity)
        {
            return string.Format(script, entity.ID, entity.Name, entity.Description);
        }

        protected override string FormatDelete(string script, Material entity)
        {
            return string.Format(script, entity.ID);
        }
    }
}
