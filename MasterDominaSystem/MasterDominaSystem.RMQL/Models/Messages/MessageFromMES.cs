using System.Text.Json.Serialization;

using MasterDominaSystem.RMQL.Models.Enums;

using Microsoft.Extensions.DependencyInjection;

namespace MasterDominaSystem.RMQL.Models.Messages
{
    public class MessageFromMES : IBrokerMessage<MessageFromMES>
    {
        [JsonPropertyName("type")]
        public MessageFromMESType Type { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        public void Handle(IServiceProvider services)
        {
            throw new NotImplementedException();
        }
    }
}
