using Microsoft.Extensions.DependencyInjection;

namespace UnitedSystems.EventBus.Interfaces
{
    /// <summary>
    /// Необходим для экранирования некоторых методов, чтобы сделать их доступными только после выполнения <see cref="AddRabbitMQEventBus"/> и подобных методов
    /// </summary>
    public interface IEventBusBuilder
    {
        IServiceCollection Services { get; set; }
    }
}
