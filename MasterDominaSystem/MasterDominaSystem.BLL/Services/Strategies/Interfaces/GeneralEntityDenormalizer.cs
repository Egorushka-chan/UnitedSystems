using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.BLL.Services.Extensions;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace MasterDominaSystem.BLL.Services.Strategies.Interfaces
{
    internal abstract class GeneralEntityDenormalizer<TEntityDB>
        : IEntityDenormalizer<TEntityDB>
        where TEntityDB : IEntityDB
    {
        protected Dictionary<string, string> ScriptsDomains { get; set; } = [];
        protected readonly DenormalizationOptions _options;
        protected readonly IProcedureBaker _procedureBaker;
        protected readonly string scriptsPath;

        protected bool isInserted = false;
        protected bool isDeleted = false;
        protected abstract string ThisName { get; }

        protected GeneralEntityDenormalizer(Action<DenormalizationOptions>? options,
            IWebHostEnvironment environment, IReportsCollector reportsCollector, IProcedureBaker procedureBaker)
        {
            _options = new();
            options?.Invoke(_options);

            _procedureBaker = procedureBaker;

            scriptsPath = Path.Combine(environment.ContentRootPath, "ScriptFiles");

            foreach(var (reportType, attributeInfo) in reportsCollector.GetReports()) {
                if (attributeInfo.Types.Contains(typeof(TEntityDB))) {
                    ScriptsDomains.Add(reportType.GetKey(), Path.Combine(scriptsPath, reportType.GetKey()));
                }
            }

            string typeName = typeof(TEntityDB).Name;
            
            var allowed = from excluded in _options.NotDenormalizeToTables
                          let key = excluded.GetKey()
                          where !ScriptsDomains.ContainsKey(key)
                          select key;
            foreach (string key in allowed) {
                ScriptsDomains.Remove(key);
            }
        }

        public async Task<string> Append(TEntityDB entityDB, Type? report = default)
        {
            string script = "";
            foreach (var (reportKey, reportPath) in ScriptsDomains) {
                if (report != null)
                    if (report.GetKey() != reportKey)
                        continue;

                script += await _procedureBaker.AssertBaked(reportKey);

                script += await AppendScriptFill(entityDB, reportKey);
            }
            if (string.IsNullOrEmpty(script))
                throw new InvalidOperationException($"Не удалось найти скрипты для файлов." +
                    $" Возможно, все отчеты исключены для {nameof(ThisName)}");
            return script;
        }

        public async Task<string> Delete(TEntityDB entityDB, Type? report = default)
        {
            string script = "";
            foreach (var (reportKey, reportPath) in ScriptsDomains) {
                if (report != null)
                    if (report.GetKey() != reportKey)
                        continue;

                script += await _procedureBaker.AssertBaked(reportKey);

                script += await DeleteScriptFill(entityDB, reportKey);
            }
            if (string.IsNullOrEmpty(script))
                throw new InvalidOperationException($"Не удалось найти скрипты для файлов." +
                    $" Возможно, все отчеты исключены для {nameof(ThisName)}");
            return script;
        }
        protected abstract Task<string> AppendScriptFill(TEntityDB entityDB, string reportKey);
        protected abstract Task<string> DeleteScriptFill(TEntityDB entityDB, string reportKey);
    }
}
