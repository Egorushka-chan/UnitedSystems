using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class PhysiqueDenormalizer(IWebHostEnvironment environment,
        IReportsCollector reportsCollector,
        IProcedureBaker procedureBaker,
        Action<DenormalizationOptions>? options = default)
        : GeneralEntityDenormalizer<Physique>(options, environment, reportsCollector, procedureBaker)
    {
        private readonly Dictionary<string, string> DeleteReportScriptName = new() {
            {typeof(ReportCloth).GetKey(), "DeletePhysiqueReportPerson" }
        };

        private readonly string insertPath = Path.Combine("Insert", "Physique.sql");
        private readonly string deletePath = Path.Combine("Delete", "Physique.sql");
        protected override string ThisName => nameof(PhysiqueDenormalizer);

        protected override async Task<string> AppendScriptFill(Physique entityDB, string reportKey)
        {
            string script = "";
            
            if (!isInserted) {
                string myId = entityDB.ID.ToString();
                string weight = entityDB.Weight.ToString();
                string growth = entityDB.Growth.ToString();
                string force = entityDB.Force.ToString();
                string description = entityDB.Description?.InSQLStringQuotes() ?? "NULL";
                string personID = entityDB.PersonID.ToString();

                string insertScript = await File.ReadAllTextAsync(Path.Combine(scriptsPath, insertPath));
                script += insertScript.Replace("{id}", myId)
                    .Replace("{weight}", weight)
                    .Replace("{growth}", growth)
                    .Replace("{force}", force)
                    .Replace("{description}", description)
                    .Replace("{personID}", personID);

                isInserted = true;
            }

            return script;
        }

        protected override async Task<string> DeleteScriptFill(Physique entityDB, string reportKey)
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
