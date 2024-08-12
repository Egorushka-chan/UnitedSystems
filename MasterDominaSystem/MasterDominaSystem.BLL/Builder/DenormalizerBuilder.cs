using Microsoft.Extensions.DependencyInjection;

namespace MasterDominaSystem.BLL.Builder
{
    internal class DenormalizerBuilder(IServiceCollection services) : IDenormalizerBuilder
    {
        public IServiceCollection Services { get; set; } = services;
        public bool GeneralStrategiesCreated { get; set; } = false;
    }
}
