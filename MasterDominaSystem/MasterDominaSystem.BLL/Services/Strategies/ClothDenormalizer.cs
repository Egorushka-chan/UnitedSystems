using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class ClothDenormalizer : GeneralEntityDenormalizer<Cloth>
    {
        /// <summary>
        /// В какие отчеты по умолчанию может идти объект
        /// </summary>
        private readonly static string[] DefaultAllowedReports = {
            typeof(ReportCloth).GetKey(),
            typeof(ReportPerson).GetKey()
        };

        private readonly Dictionary<string, string> ReportScriptsAppend = [];
        public ClothDenormalizer(IWebHostEnvironment environment, Action<DenormalizationOptions>? options = default)
            : base(options, environment)
        {
            var allowed = from excluded in _options.NotDenormalizeToTables
                          let key = excluded.GetKey()
                          where !DefaultAllowedReports.Contains(key)
                          select key;
            foreach (string key in allowed) {
                string filePath = ScriptsDomains[key] + nameof(Cloth);
                string scriptBody = File.ReadAllText(filePath);
                ReportScriptsAppend.Add(key, scriptBody);
            }
        }

        private bool CheckSemicolonEnding(string value)
        {
            value = value.Trim();
            int length = value.Length;
            for (int i = length - 1; i >= 0; i--) {
                char letter = value[i];
                if (letter == ';') {
                    return true;
                }
                else if (char.IsWhiteSpace(letter)) {
                    continue;
                }
                return false;
            }
            return false;
        }

        public override string Append(Cloth entity)
        {
            string final = UniteScriptsAppend();
            return final;
        }

        private string UniteScriptsAppend()
        {
            string final = "";
            foreach (var (key, script) in ReportScriptsAppend) {
                if (!CheckSemicolonEnding(script)) {
                    final += ';' + script;
                }
                else {
                    final += script;
                }
            }
            return final;
        }

        public override string Delete(Cloth entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(Cloth entity)
        {
            throw new NotImplementedException();
        }
    }
}
