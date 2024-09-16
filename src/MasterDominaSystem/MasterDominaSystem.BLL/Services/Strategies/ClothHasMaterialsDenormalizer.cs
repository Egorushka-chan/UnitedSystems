using MasterDominaSystem.BLL.Services.Strategies.Interfaces;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class ClothHasMaterialsDenormalizer(IWebHostEnvironment environment) : IEntityDenormalizer<ClothHasMaterials>
    {
        protected string ThisName => nameof(SetHasClothesDenormalizer);
        protected readonly string insertPath = Path.Combine("Insert", "ClothHasMaterial.sql");
        protected readonly string deletePath = Path.Combine("Delete", "ClothHasMaterial.sql");
        protected readonly string scriptsPath = Path.Combine(environment.ContentRootPath, "ScriptFiles");

        public async Task<string> Append(ClothHasMaterials entityDB, Type? report = null)
        {
            string script = await File.ReadAllTextAsync(Path.Combine(scriptsPath, insertPath));
            script = script.Replace("{id}", entityDB.ID.ToString())
                .Replace("{materialID}", entityDB.MaterialID.ToString())
                .Replace("{clothID}", entityDB.ClothID.ToString());
            return script;
        }

        public async Task<string> Delete(ClothHasMaterials entityDB, Type? report = null)
        {
            string script = await File.ReadAllTextAsync(Path.Combine(scriptsPath, deletePath));
            script = script.Replace("{id}", entityDB.ID.ToString());
            return script;
        }
    }
}
