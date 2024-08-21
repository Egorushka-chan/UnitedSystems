using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class PhotoDenormalizer(IWebHostEnvironment environment, IReportsCollector reportsCollector,
        IProcedureBaker procedureBaker,
        Action<DenormalizationOptions>? options = default)
        : GeneralEntityDenormalizer<Photo>(options, environment, reportsCollector, procedureBaker)
    {

        private Dictionary<string, string> AppendReportScriptName = new() {
            {typeof(ReportCloth).GetKey(), "assertphoto_reportcloth" },
            {typeof(ReportPerson).GetKey(), "assertphoto_reportperson" }
        };

        private Dictionary<string, string> DeleteReportScriptName = new() {
            {typeof(ReportCloth).GetKey(), "deletephoto_reportcloth" },
            {typeof(ReportPerson).GetKey(), "deletephoto_reportperson" }
        };
        protected override string ThisName => nameof(PhotoDenormalizer);

        protected override string AppendScriptFill(Photo entityDB, string reportKey)
        {
            string script = "";
            string call = AppendReportScriptName[reportKey];
            if (reportKey == typeof(ReportCloth).GetKey()) {
                int myId = entityDB.ID;
                string myName = entityDB.Name.InSQLStringQuotes();
                string photoHash = entityDB.HashCode.InSQLStringQuotes();

                string rawScript = $"CALL {call}({myId}, {myName}, {photoHash});";

                script += rawScript;
            }
            else if (reportKey == typeof(ReportPerson).GetKey()) {

            }
            return script;
        }

        protected override string DeleteScriptFill(Photo entityDB, string reportKey)
        {
            throw new NotImplementedException();
        }
    }
}
