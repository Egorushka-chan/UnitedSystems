﻿using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class MaterialDenormalizer(IWebHostEnvironment environment, IReportsCollector reportsCollector,
        IProcedureBaker procedureBaker,
        Action<DenormalizationOptions>? options = default)
        : GeneralEntityDenormalizer<Material>(options, environment, reportsCollector, procedureBaker)
    {
        private readonly Dictionary<string, string> AppendReportScriptName = new() {
            {typeof(ReportCloth).GetKey(), "AssertMaterialReportCloth" }
        };
        protected override string ThisName => typeof(MaterialDenormalizer).Name;
        private readonly string insertPath = Path.Combine("Insert", "Material.sql");
        private readonly string deletePath = Path.Combine("Delete", "Material.sql");

        protected override async Task<string> AppendScriptFill(Material entityDB, string reportKey)
        {
            string script = "";
            if (reportKey == typeof(ReportCloth).GetKey()) {
                string call = AppendReportScriptName[reportKey];
                string myId = entityDB.ID.ToString();
                string myName = entityDB.Name.InSQLStringQuotes();
                string myDescription = entityDB.Description?.InSQLStringQuotes() ?? "NULL";

                string rawScript = $"CALL {call}({myId}, {myName}, {myDescription});";

                script += rawScript;

                if (!isInserted) {
                    string insertScript = await File.ReadAllTextAsync(Path.Combine(scriptsPath, insertPath));
                    script += insertScript.Replace("{id}", myId)
                        .Replace("{name}", myName)
                        .Replace("{description}", myDescription);

                    isInserted = true;
                }
            }

            return script;
        }

        protected override async Task<string> DeleteScriptFill(Material entityDB, string reportKey)
        {
            string script = "";
            string myId = entityDB.ID.ToString();

            if (!isDeleted) {
                string tableScript = await File.ReadAllTextAsync(Path.Combine(scriptsPath, deletePath));
                script += tableScript.Replace("{id}", myId);
                isDeleted = true;
            }

            return script;
        }
    }
}
