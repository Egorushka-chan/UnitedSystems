using System.Text.Json;

namespace UnitedSystems.EventBus.Models
{
    public class EventBusSubscriptionInfo
    {
        public Dictionary<string, Type> EventTypes { get; set; } = [];
        public JsonSerializerOptions JsonSerializerOptions { get; set; } = JsonSerializerOptions.Default;
    }
}
