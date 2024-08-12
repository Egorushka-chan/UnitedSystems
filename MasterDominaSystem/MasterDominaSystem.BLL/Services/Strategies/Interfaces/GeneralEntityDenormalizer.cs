using MasterDominaSystem.BLL.Builder;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace MasterDominaSystem.BLL.Services.Strategies.Interfaces
{
    internal abstract class GeneralEntityDenormalizer<TEntityDB>
        : IEntityDenormalizer<TEntityDB>
        where TEntityDB : IEntityDB
    {
        private readonly string _scriptsPath;
        protected GeneralEntityDenormalizer(Action<DenormalizationOptions>? options,
            IWebHostEnvironment environment)
        {
            _options = new();
            options?.Invoke(_options);

            string dir = environment.ContentRootPath;
            _scriptsPath = Path.Combine(dir, "ScriptFiles");
        }

        private readonly DenormalizationOptions _options;

        public abstract string Append(TEntityDB entity);

        public abstract string Delete(TEntityDB entity);

        public abstract string Update(TEntityDB entity);
    }
}
