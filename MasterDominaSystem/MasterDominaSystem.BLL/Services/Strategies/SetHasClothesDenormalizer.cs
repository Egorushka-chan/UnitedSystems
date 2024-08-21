using MasterDominaSystem.BLL.Services.Strategies.Interfaces;

using Microsoft.AspNetCore.Hosting;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.BLL.Services.Strategies
{
    internal class SetHasClothesDenormalizer(IWebHostEnvironment environment) : IEntityDenormalizer<SetHasClothes>
    {
        protected string ThisName => nameof(SetHasClothesDenormalizer);
        protected readonly string insertPath = Path.Combine("Insert", "insertsethascloth.sql");
        protected readonly string deletePath = Path.Combine("Delete", "deletesethascloth.sql");
        protected readonly string scriptsPath = Path.Combine(environment.ContentRootPath, "ScriptFiles");

        public async Task<string> Append(SetHasClothes entityDB, Type? report = null)
        {
            string script = await File.ReadAllTextAsync(Path.Combine(scriptsPath, insertPath));
            script = script.Replace("{id}", entityDB.ID.ToString())
                .Replace("{setID}", entityDB.SetID.ToString())
                .Replace("{clothID}", entityDB.ClothID.ToString());
            return script;
        }

        public async Task<string> Delete(SetHasClothes entityDB, Type? report = null)
        {
            string script = await File.ReadAllTextAsync(Path.Combine(scriptsPath, deletePath));
            script = script.Replace("{id}", entityDB.ID.ToString());
            return script;
        }
    }
}
