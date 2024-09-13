using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class PhotoDenormalizer(IWebHostEnvironment environment,
        IReportsCollector reportsCollector,
        IProcedureBaker procedureBaker,
        Action<DenormalizationOptions>? options = default)
        : GeneralEntityDenormalizer<Photo>(options, environment, reportsCollector, procedureBaker)
    {

        private readonly Dictionary<string, string> AppendReportScriptName = new() {
            {typeof(ReportCloth).GetKey(), "AssertPhotoReportCloth" }
        };

        private readonly Dictionary<string, string> DeleteReportScriptName = new() {
            {typeof(ReportCloth).GetKey(), "DeletePhotoReportCloth" },
            {typeof(ReportPerson).GetKey(), "DeletePhotoReportPerson" }
        };

        private readonly string insertPath = Path.Combine("Insert", "Photo.sql");
        private readonly string deletePath = Path.Combine("Delete", "Photo.sql");
        protected override string ThisName => nameof(PhotoDenormalizer);

        protected override async Task<string> AppendScriptFill(Photo entityDB, string reportKey)
        {
            string script = "";
            
            string myId = entityDB.ID.ToString();
            string myName = entityDB.Name.InSQLStringQuotes();
            string photoHash = entityDB.HashCode.InSQLStringQuotes();

            if (reportKey == typeof(ReportCloth).GetKey()) {
                string call = AppendReportScriptName[reportKey];
                string rawScript = $"CALL {call}({myId}, {myName}, {photoHash});";
                script += rawScript;
            }

            if (!isInserted) {
                string insertScript = await File.ReadAllTextAsync(Path.Combine(scriptsPath, insertPath));
                script += insertScript.Replace("{id}", myId)
                    .Replace("{name}", myName)
                    .Replace("{hashcode}", photoHash)
                    .Replace("{clothID}", entityDB.ClothID.ToString());

                isInserted = true;
            }
            return script;
        }

        protected override async Task<string> DeleteScriptFill(Photo entityDB, string reportKey)
        {
            string script = "";
            string myId = entityDB.ID.ToString();

            string path = Path.Combine(scriptsPath, reportKey, DeleteReportScriptName[reportKey]);
            string rawScript = await File.ReadAllTextAsync(path);

            script += rawScript.Replace("{id}", myId);

            if (!isDeleted) {
                string tableScript = await File.ReadAllTextAsync(Path.Combine(scriptsPath, deletePath));
                script += tableScript.Replace("{id}", myId);
                isDeleted = true;
            }
            
            return script;
        }
    }
}
