using UnitedSystems.CommonLibrary.Queries;

namespace ManyEntitiesSender.BPL.Abstraction
{
    public interface IMDMSender
    {
        public void Send(string message);
        void Send(object obj);
    }
}
