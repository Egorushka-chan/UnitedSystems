using ManyEntitiesSender.BPL.Abstraction;

using UnitedSystems.CommonLibrary.Models.ManyEntitiesSender.Messages.Headers;
using UnitedSystems.CommonLibrary.Queries;

namespace ManyEntitiesSender.BPL.Implementation
{
    public class KafkaSender : IMDMSender
    {
        public void Send(string message, MessageHeaderFromMES header = MessageHeaderFromMES.NotSpecified)
        {
            throw new NotImplementedException();
        }

        public void Send(object obj, MessageHeaderFromMES header = MessageHeaderFromMES.NotSpecified)
        {
            throw new NotImplementedException();
        }
    }
}
