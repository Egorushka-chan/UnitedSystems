using System.Text.Json.Serialization;

using UnitedSystems.CommonLibrary.Models.ManyEntitiesSender.Messages.Headers;

namespace UnitedSystems.CommonLibrary.Models.MasterDominaSystem.Messages
{
    public class MessageFromMES
    {
        [JsonPropertyName("type")]
        public MessageHeaderFromMES Type { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}
