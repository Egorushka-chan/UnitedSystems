﻿using MasterDominaSystem.BLL.Builder;
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
        protected Dictionary<string, string> ScriptsDomains;
        protected readonly DenormalizationOptions _options;
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
        }

        public abstract string Append(TEntityDB entity);

        public abstract string Delete(TEntityDB entity);

        public abstract string Update(TEntityDB entity);
    }
}
