using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class PhotoDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default) 
        : GeneralEntityDenormalizer<Photo>(options, environment)
    {
        protected override string[] DefaultAllowedReports { get; set; } = [
            typeof(ReportCloth).GetKey(),
            typeof(ReportPerson).GetKey()
        ];

        protected override string FormatAppend(string script, Photo entity)
        {
            return string.Format(script, entity.ID, entity.Name, entity.HashCode, entity.IsDBStored, entity.ClothID);
        }

        protected override string FormatDelete(string script, Photo entity)
        {
            return string.Format(script, entity.ID);
        }
    }
}
