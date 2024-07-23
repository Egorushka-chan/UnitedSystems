using System.Text.Json.Serialization;

using MasterDominaSystem.RMQL.Models.Enums;

using Microsoft.Extensions.DependencyInjection;

namespace MasterDominaSystem.RMQL.Models.Messages
{
    public class MessageFromWO : IBrokerMessage<MessageFromWO>
    {
        [JsonPropertyName("type")]
        public MessageFromWOType Type { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        public void Handle(IServiceProvider services)
        {
            throw new NotImplementedException();
        }
    }
}
