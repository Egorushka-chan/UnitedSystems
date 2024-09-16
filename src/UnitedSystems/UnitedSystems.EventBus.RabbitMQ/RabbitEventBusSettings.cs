﻿namespace UnitedSystems.EventBus.RabbitMQ
{
    public class RabbitEventBusSettings
    {
        public string ServiceQueueName { get; set; } = "NotSpecified";
        public string UserName { get; set; } = "root";
        public string Password { get; set; } = "tobacco";
        public int RetryCount = 5;
    }
}