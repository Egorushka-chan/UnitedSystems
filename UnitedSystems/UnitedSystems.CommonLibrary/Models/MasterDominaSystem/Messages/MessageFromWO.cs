using System.Text.Json.Serialization;

using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages.Headers;

namespace UnitedSystems.CommonLibrary.Models.MasterDominaSystem.Messages
{
    public class MessageFromWO
    {
        [JsonPropertyName("type")]
        public MessageHeaderFromWO Type { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}
