using ManyEntitiesSender.PL.Enums;

namespace ManyEntitiesSender.BPL.Abstraction
{
    public interface IBrokerSender
    {
        public void Send(string message, RabbitQueueType queueType);
        void Send(object obj, RabbitQueueType queueType);
    }
}
