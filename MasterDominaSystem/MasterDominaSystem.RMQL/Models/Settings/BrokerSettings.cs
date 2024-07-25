using Microsoft.Extensions.Options;

namespace ManyEntitiesSender.PL.Settings
{
    public class BrokerSettings : IOptions<BrokerSettings>
    {
        public string User { get; set; } = "root";
        public string Password { get; set; } = "tobacco";
        public string ConnectionString { get; set; } = "RabbitMQ";
        BrokerSettings IOptions<BrokerSettings>.Value => this;
    }
}
