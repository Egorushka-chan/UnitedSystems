namespace ManyEntitiesSender.BPL.Abstraction
{
    public interface IBrokerSender
    {
        public Task SendAsync(string message, CancellationToken cancellationToken = default);
        public void Send(string message);
    }
}
