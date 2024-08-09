using Microsoft.Extensions.Logging;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.DAL
{
    internal class JsonDBSeeder(ILogger<JsonDBSeeder> logger) : IDBSeeder
    {
        private readonly Dictionary<string, Type> JsonPairs = new() {
            {"cloth", typeof(Cloth) },
            {"material", typeof(Material) },
            {"persons", typeof(Person) },
            {"physiques", typeof(Physique) },
            {"seasons", typeof(Season) },
            {"sets", typeof(Set) },
        };

        public Task Seed(IWardrobeContext context)
        {
            throw new NotImplementedException();
            foreach((string key, Type keyType) in JsonPairs) {
                
            }
        }
    }
}
