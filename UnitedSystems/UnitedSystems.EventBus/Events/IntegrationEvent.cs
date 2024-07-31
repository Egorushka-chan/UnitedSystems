using System.Text.Json.Serialization;

namespace UnitedSystems.EventBus.Events
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            ID = Guid.NewGuid();
            TimeStamp = DateTime.Now;
        }

        [JsonPropertyName("id")]
        public Guid ID { get; set; }
        [JsonPropertyName("stamp")]
        public DateTime TimeStamp { get; set; }
    }
}