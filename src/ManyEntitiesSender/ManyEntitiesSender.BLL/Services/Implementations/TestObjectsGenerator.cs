using ManyEntitiesSender.BLL.Services.Abstractions;
using ManyEntitiesSender.BLL.Settings;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using UnitedSystems.CommonLibrary.ManyEntitiesSender;

namespace ManyEntitiesSender.DAL
{
    public class TestObjectsGenerator(IServiceScopeFactory scopeFactory, IOptions<PackageSettings> options, ILogger<AbsObjectGenerator> logger) : AbsObjectGenerator(scopeFactory, options, logger)
    {
        protected override Body CreateBody(int testNo)
        {
            return new Body
            {
                Mightiness = $"Mightiness #{testNo}"
            };
        }

        protected override Hand CreateHand(int testNo)
        {
            return new Hand
            {
                State = $"State #{testNo}"
            };
        }

        protected override Leg CreateLeg(int testNo)
        {
            return new Leg
            {
                State = $"State #{testNo}"
            };
        }
    }
}
