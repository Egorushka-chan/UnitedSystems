using ManyEntitiesSender.BPL.Abstraction;

using UnitedSystems.CommonLibrary.Queries;

namespace ManyEntitiesSender.BPL.Implementation
{
    public class KafkaSender : IMDMSender
    {
        public void Send(string message)
        {
            throw new NotImplementedException();
        }

        public void Send(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
