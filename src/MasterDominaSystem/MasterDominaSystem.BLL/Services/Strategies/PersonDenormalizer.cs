using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class PersonDenormalizer(IWebHostEnvironment environment, IReportsCollector reportsCollector,
        IProcedureBaker procedureBaker,
        Action<DenormalizationOptions>? options = default)
        : GeneralEntityDenormalizer<Person>(options, environment, reportsCollector, procedureBaker)
    {
        private readonly Dictionary<string, string> AppendReportScriptName = new() {
            {typeof(ReportCloth).GetKey(), "AssertPersonReportPerson" }
        };

        private readonly Dictionary<string, string> DeleteReportScriptName = new() {
            {typeof(ReportPerson).GetKey(), "DeletePersonReportPerson" }
        };

        private readonly string insertPath = Path.Combine("Insert", "Person.sql");
        private readonly string deletePath = Path.Combine("Delete", "Person.sql");
        protected override string ThisName => nameof(PersonDenormalizer);

        protected override async Task<string> AppendScriptFill(Person entityDB, string reportKey)
        {
            string script = "";
            
            string myId = entityDB.ID.ToString();

            if (reportKey == typeof(ReportPerson).GetKey()) {
                string call = AppendReportScriptName[reportKey];
                string rawScript = $"CALL {call}({myId});";

                script += rawScript;
            }

            if (!isInserted) {
                string insertScript = await File.ReadAllTextAsync(Path.Combine(scriptsPath, insertPath));

                string myName = entityDB.Name.InSQLStringQuotes();
                string myType = entityDB.Type?.InSQLStringQuotes() ?? "NULL";

                script += insertScript.Replace("{id}", myId)
                    .Replace("{name}", myName)
                    .Replace("{type}", myType);

                isInserted = true;
            }
            return script;
        }

        protected override async Task<string> DeleteScriptFill(Person entityDB, string reportKey)
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
