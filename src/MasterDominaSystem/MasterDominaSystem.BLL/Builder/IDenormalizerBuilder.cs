using Microsoft.Extensions.DependencyInjection;

namespace MasterDominaSystem.BLL.Builder
{
    public interface IDenormalizerBuilder
    {
        IServiceCollection Services { get; set; }
        bool GeneralStrategiesCreated { get; internal set; }
    }
}
