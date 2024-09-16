using System.Text.Json;

using ManyEntitiesSender.BLL.Models;
using ManyEntitiesSender.BLL.Services.Abstractions;
using ManyEntitiesSender.BLL.Settings;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using UnitedSystems.CommonLibrary.ManyEntitiesSender;

namespace ManyEntitiesSender.BLL.Services.Implementations
{
    /// <summary>
    /// Класс заполняет базу основываясь на namesObjectsGenerator.json в папке internalFiles
    /// </summary>
    public class RandomObjectGenerator: AbsObjectGenerator
    {
        private readonly NamesObjectsGenerator _properties;
        private readonly Random _random = new(123);
        public RandomObjectGenerator(IServiceScopeFactory scopeFactory, IOptions<PackageSettings> options, ILogger<AbsObjectGenerator> logger) : base(scopeFactory, options, logger)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "internalFiles", "namesObjectsGenerator.json");
            if (!Path.Exists(path))
                throw new FileNotFoundException("RandomObjectGenerator cannot be used without namesObjectsGenerator.json configuration file");

            NamesObjectsGenerator? result;

            string json = File.ReadAllText(path);
            result = JsonSerializer.Deserialize<NamesObjectsGenerator>(json);


            if (result == null)
                throw new FileLoadException("namesObjectsGenerator.json is null");
            _properties = result;
        }

        protected override Body CreateBody(int testNo)
        {
            int selected = _random.Next(0, 10);
            if(_properties.Mightiness != null)
            {
                selected = _random.Next(0, _properties.Mightiness.Length);
            }
            
            string mightiness = "NULL";
            if(_properties.Mightiness != null)
            {
                mightiness = _properties.Mightiness[selected];
            }

            return new Body()
            {
                Mightiness = mightiness
            };
        }

        protected override Hand CreateHand(int testNo)
        {
            int selected = _random.Next(0, 10);
            if(_properties.Mightiness != null)
            {
                selected = _random.Next(0, _properties.Mightiness.Length);
            }

            string state = "NULL";
            if(_properties.Mightiness != null)
            {
                state = _properties.Mightiness[selected];
            }

            return new Hand()
            {
                State = state
            };
        }

        protected override Leg CreateLeg(int testNo)
        {
            int selected = _random.Next(0, 10);
            if(_properties.Mightiness != null)
            {
                selected = _random.Next(0, _properties.Mightiness.Length);
            }

            string state = "NULL";
            if(_properties.Mightiness != null)
            {
                state = _properties.Mightiness[selected];
            }

            return new Leg()
            {
                State = state
            };
        }
    }
}
