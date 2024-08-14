using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class PhysiqueDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default) 
        : GeneralEntityDenormalizer<Physique>(options, environment)
    {
        protected override string[] DefaultAllowedReports { get; set; } = [
            typeof(ReportPerson).GetKey()
        ];

        protected override string FormatAppend(string script, Physique entity)
        {
            return string.Format(script, entity.ID, entity.Growth, entity.Weight, entity.Force, entity.Description, entity.PersonID);
        }

        protected override string FormatDelete(string script, Physique entity)
        {
            return string.Format(script, entity.ID);
        }
    }
}
