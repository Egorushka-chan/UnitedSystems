using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.DAL
{
    internal partial class JsonDBSeeder(ILogger<JsonDBSeeder> logger,
        IWebHostEnvironment env,
        IWardrobeContext context) : IDBSeeder
    {
        private readonly string _contentRootPath = Directory.GetCurrentDirectory();

        private readonly Dictionary<string, Type> JsonPairs = new() {
            {"cloth.json", typeof(ClothesJson) },
            {"material.json", typeof(MaterialJson) },
            {"persons.json", typeof(PersonsJson) },
            {"physiques.json", typeof(PhysiqueJson) },
            {"seasons.json", typeof(SeasonJson) },
            {"sets.json", typeof(SetJson) },
        };

        public async Task Seed()
        {
            string directory = Path.Combine(_contentRootPath, "Setup");
            foreach ((string key, Type keyType) in JsonPairs) {
                string path = Path.Combine(directory, key);
                string content = File.ReadAllText(path);
                object objects = JsonSerializer.Deserialize(content, keyType) ?? throw new InvalidOperationException($"Отсутствует контент в файле {path}");
                MyJson myJson = (MyJson)objects;
                await myJson.Append(context);
            }

            await context.SaveChangesAsync();
        }

        private abstract class MyJson
        {
            public abstract Task Append(IWardrobeContext context);
        }
        private class ClothesJson : MyJson
        {
            [JsonPropertyName("clothes")]
            public Cloth[] Clothes { get; set; } = [];

            public override async Task Append(IWardrobeContext context)
            {
                await context.Clothes.AddRangeAsync(Clothes);
            }
        }
        private class MaterialJson : MyJson
        {
            [JsonPropertyName("materials")]
            public Material[] Materials { get; set; } = [];

            public override async Task Append(IWardrobeContext context)
            {
                await context.Materials.AddRangeAsync(Materials);
            }
        }
        private class PersonsJson : MyJson
        {
            [JsonPropertyName("persons")]
            public Person[] Persons { get; set; } = [];

            public override async Task Append(IWardrobeContext context)
            {
                await context.Persons.AddRangeAsync(Persons);
            }
        }
        private class PhysiqueJson : MyJson
        {
            [JsonPropertyName("physiques")]
            public Physique[] Physiques { get; set; } = [];

            public override async Task Append(IWardrobeContext context)
            {
                await context.Physiques.AddRangeAsync(Physiques);
            }
        }
        private class SeasonJson : MyJson
        {
            [JsonPropertyName("seasons")]
            public Season[] Seasons { get; set; } = [];

            public override async Task Append(IWardrobeContext context)
            {
                await context.Seasons.AddRangeAsync(Seasons);
            }
        }
        private class SetJson : MyJson
        {
            [JsonPropertyName("sets")]
            public Set[] Sets { get; set; } = [];

            public override async Task Append(IWardrobeContext context)
            {
                await context.Sets.AddRangeAsync(Sets);
            }
        }
    }
}
