using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class ClothDenormalizer(IWebHostEnvironment environment,
        IReportsCollector reportsCollector,
        IProcedureBaker procedureBaker,
        Action<DenormalizationOptions>? options = default) : GeneralEntityDenormalizer<Cloth>(options, environment, reportsCollector, procedureBaker)
    {
        private Dictionary<string, string> AppendReportScriptName = new() {
            {typeof(ReportCloth).GetKey(), "AssertClothReportCloth" }
        };

        private Dictionary<string, string> DeleteReportScriptName = new() {
            {typeof(ReportCloth).GetKey(), "DeleteClothReportCloth" },
            {typeof(ReportPerson).GetKey(), "DeleteClothReportPerson" }
        };

        private readonly string insertPath = Path.Combine("Insert", "Cloth.json");
        private readonly string deletePath = Path.Combine("Delete", "Cloth.json");

        protected override string ThisName => nameof(ClothDenormalizer);
        protected override async Task<string> AppendScriptFill(Cloth entityDB, string reportKey)
        {    
            string script = "";
            string call = AppendReportScriptName[reportKey];
            if (reportKey == typeof(ReportCloth).GetKey()) {
                string myId = entityDB.ID.ToString();
                string myName = entityDB.Name.InSQLStringQuotes();
                string myDescription = entityDB.Description?.InSQLStringQuotes() ?? "NULL";
                string myRating = entityDB.Rating.ToString() ?? "NULL";
                string mySize = entityDB.Size?.InSQLStringQuotes() ?? "NULL";
                string materialIDs = entityDB.ClothHasMaterials.Select(chm => chm.MaterialID).ToArray().ToSQLArray();
                string photoIDs = entityDB.Photos.Select(ph => ph.ClothID).ToSQLArray();

                string rawScript = $"CALL {call}({myId}, {myName}, {myDescription}, {myRating}, {mySize}, " +
                    $"{materialIDs}, {photoIDs});";

                script += rawScript;

                if (!isInserted) {
                    string insertScript = await File.ReadAllTextAsync(Path.Combine(scriptsPath, insertPath));
                    script += insertScript.Replace("{id}", myId)
                        .Replace("{name}", myName)
                        .Replace("{description}", myDescription)
                        .Replace("{rating}", myRating)
                        .Replace("{size}", mySize);

                    isInserted = true;
                }
            }

            return script;
        }

        protected override async Task<string> DeleteScriptFill(Cloth entityDB, string reportKey)
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
