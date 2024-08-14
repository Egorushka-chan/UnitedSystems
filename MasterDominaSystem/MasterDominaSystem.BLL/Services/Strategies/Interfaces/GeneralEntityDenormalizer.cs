using System.Runtime.CompilerServices;

using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.DAL.Reports;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace MasterDominaSystem.BLL.Services.Strategies.Interfaces
{
    internal abstract class GeneralEntityDenormalizer<TEntityDB>
        : IEntityDenormalizer<TEntityDB>
        where TEntityDB : IEntityDB
    {
        protected Dictionary<string, string> ScriptsDomains;
        protected readonly DenormalizationOptions _options;

        /// <summary>
        /// В какие отчеты по умолчанию может идти объект
        /// </summary>
        protected abstract string[] DefaultAllowedReports { get; set; }

        protected const string AppendPath = "Append";
        protected const string DeletePath = "Delete";

        protected readonly Dictionary<string, string> ReportScriptsDelete = [];
        protected readonly Dictionary<string, string> ReportScriptsAppend = [];
        protected GeneralEntityDenormalizer(Action<DenormalizationOptions>? options,
            IWebHostEnvironment environment)
        {
            _options = new();
            options?.Invoke(_options);

            string dir = environment.ContentRootPath;
            string scriptsPath = Path.Combine(dir, "ScriptFiles");

            ScriptsDomains = new() {
                {typeof(ReportPerson).GetKey(), Path.Combine(scriptsPath, "mergeToReportPerson_") },
                {typeof(ReportCloth).GetKey(), Path.Combine(scriptsPath, "mergeToReportCloth_") }
            };

            string typeName = typeof(TEntityDB).Name;

            var allowed = from excluded in _options.NotDenormalizeToTables
                          let key = excluded.GetKey()
                          where !DefaultAllowedReports.Contains(key)
                          select key;
            foreach (string key in allowed) {
                string appendPath = Path.Combine(AppendPath, ScriptsDomains[key] + nameof(typeName));
                string deletePath = Path.Combine(DeletePath, ScriptsDomains[key] + nameof(typeName));

                Task<string> textAppend = File.ReadAllTextAsync(appendPath);
                Task<string> textDelete = File.ReadAllTextAsync(deletePath);
                Task.WaitAll(textAppend, textDelete);
                string scriptBodyAppend = textAppend.Result;
                string scriptBodyUpdate = textDelete.Result;
                ReportScriptsAppend.Add(key, scriptBodyAppend);
            }
        }

        protected bool CheckSemicolonEnding(string value)
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

        public virtual string Append(TEntityDB entityDB)
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
            return FormatAppend(final, entityDB);
        }

        public virtual string Delete(TEntityDB entityDB)
        {
            string final = "";
            foreach (var (key, script) in ReportScriptsDelete) {
                if (!CheckSemicolonEnding(script)) {
                    final += ';' + script;
                }
                else {
                    final += script;
                }
            }
            return FormatDelete(final, entityDB);
        }

        protected abstract string FormatAppend(string script, TEntityDB entity);

        protected abstract string FormatDelete(string script, TEntityDB entity);
    }
}
