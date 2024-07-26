﻿using Microsoft.Extensions.Options;

namespace MasterDominaSystem.RMQL.Models.Settings
{
    public class BrokerSettings : IOptions<BrokerSettings>
    {
        public string User { get; set; } = "root";
        public string Password { get; set; } = "tobacco";
        public string ConnectionString { get; set; } = "RabbitMQ";
        BrokerSettings IOptions<BrokerSettings>.Value => this;
    }
}
