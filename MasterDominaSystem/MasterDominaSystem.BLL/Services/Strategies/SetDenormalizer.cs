using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class SetDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default)
        : GeneralEntityDenormalizer<Set>(options, environment)
    {
        protected override string[] DefaultAllowedReports { get; set; } = [
            typeof(ReportPerson).GetKey()
        ];

        protected override string FormatAppend(string script, Set entity)
        {
            return string.Format(script, entity.ID, entity.Name, entity.Description);
        }

        protected override string FormatDelete(string script, Set entity)
        {
            return string.Format(script, entity.ID);
        }
    }
}
