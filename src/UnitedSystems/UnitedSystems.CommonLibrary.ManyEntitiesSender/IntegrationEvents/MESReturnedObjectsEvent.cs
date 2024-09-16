using System.Text.Json.Serialization;
using UnitedSystems.CommonLibrary.Messages;

namespace UnitedSystems.CommonLibrary.ManyEntitiesSender.IntegrationEvents
{
    public class MESReturnedObjectsEvent : IntegrationEvent
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
    }
}
