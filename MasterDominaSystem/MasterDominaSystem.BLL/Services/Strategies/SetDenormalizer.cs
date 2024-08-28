using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class SetDenormalizer(IWebHostEnvironment environment,
        IReportsCollector reportsCollector,
        IProcedureBaker procedureBaker,
        Action<DenormalizationOptions>? options = default)
        : GeneralEntityDenormalizer<Set>(options, environment, reportsCollector, procedureBaker)
    {
        private Dictionary<string, string> DeleteReportScriptName = new() {
            {typeof(ReportCloth).GetKey(), "DeleteSetReportPerson" }
        };

        private readonly string insertPath = Path.Combine("Insert", "Set.sql");
        private readonly string deletePath = Path.Combine("Delete", "Set.sql");

        protected override string ThisName => nameof(SetDenormalizer);

        protected override async Task<string> AppendScriptFill(Set entityDB, string reportKey)
        {
            string script = "";
            
            if (!isInserted) {
                string myId = entityDB.ID.ToString();
                string myName = entityDB.Name.InSQLStringQuotes();
                string myDescription = entityDB.Description?.InSQLStringQuotes() ?? "NULL";
                string physiqueID = entityDB.PhysiqueID.ToString();
                string seasonID = entityDB.SeasonID.ToString();

                string insertScript = await File.ReadAllTextAsync(Path.Combine(scriptsPath, insertPath));
                script += insertScript.Replace("{id}", myId)
                    .Replace("{name}", myName)
                    .Replace("{description}", myDescription)
                    .Replace("{physiqueID}", physiqueID)
                    .Replace("{seasonID}", seasonID);

                isInserted = true;
            }

            return script;
        }

        protected override async Task<string> DeleteScriptFill(Set entityDB, string reportKey)
        {
            string script = "";

            string myId = entityDB.ID.ToString();
            string path = Path.Combine(scriptsPath, reportKey, DeleteReportScriptName[reportKey]);
            string rawScript = await File.ReadAllTextAsync(path);

            script += rawScript.Replace("{id}", myId);

            if (!isDeleted) {
                string deleteScript = await File.ReadAllTextAsync(Path.Combine(scriptsPath, deletePath));
                script += deleteScript.Replace("{id}", myId);

                isInserted = true;
            }

            return script;
        }
    }
}
