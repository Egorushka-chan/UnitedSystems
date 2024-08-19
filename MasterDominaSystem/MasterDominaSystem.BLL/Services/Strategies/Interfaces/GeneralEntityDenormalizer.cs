using System.Reflection;

using MasterDominaSystem.BLL.Builder;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.DAL.Reports;

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

        protected GeneralEntityDenormalizer(Action<DenormalizationOptions>? options,
            IWebHostEnvironment environment)
        {
            _options = new();
            options?.Invoke(_options);

            string scriptsPath = Path.Combine(environment.ContentRootPath, "ScriptFiles");

            var queries = from assembly in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
                          from type in assembly.GetTypes()
                          let attributes = type.GetCustomAttributes<ReportAttribute>(true)
                          where attributes != null && attributes.Any()
                          select new { Type = type, Attributes = attributes.Cast<ReportAttribute>() };
            
            foreach(var query in queries) {
                Type reportType = query.Type;
                ReportAttribute attributeInfo = query.Attributes.Single();
                if (attributeInfo.Types.Contains(typeof(TEntityDB))) {
                    ScriptsDomains.Add(reportType.GetKey(), Path.Combine(scriptsPath, reportType.GetKey()));
                }
            }

            string typeName = typeof(TEntityDB).Name;

            var allowed = from excluded in _options.NotDenormalizeToTables
                          let key = excluded.GetKey()
                          where !DefaultAllowedReports.Contains(key)
                          select key;
            foreach (string key in allowed) {

            }
        }

        //protected bool CheckSemicolonEnding(string value)
        //{
        //    value = value.Trim();
        //    int length = value.Length;
        //    for (int i = length - 1; i >= 0; i--) {
        //        char letter = value[i];
        //        if (letter == ';') {
        //            return true;
        //        }
        //        else if (char.IsWhiteSpace(letter)) {
        //            continue;
        //        }
        //        return false;
        //    }
        //    return false;
        //}

        public abstract string Append(TEntityDB entityDB);

        public abstract string Delete(TEntityDB entityDB);
    }
}
