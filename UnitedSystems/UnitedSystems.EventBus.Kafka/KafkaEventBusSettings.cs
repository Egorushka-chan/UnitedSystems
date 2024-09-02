namespace UnitedSystems.EventBus.Kafka
{
    public class KafkaEventBusSettings
    {
        public string ServiceQueueName { get; set; } = "NotSpecified";
        public string HostName { get; set; } = "Kafka";
        public string UserName { get; set; } = "root";
        public string Password { get; set; } = "tobacco";
        public int RetryCount = 5;
    }
}
