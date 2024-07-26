using UnitedSystems.CommonLibrary.Models.ManyEntitiesSender.Messages.Headers;
using UnitedSystems.CommonLibrary.Queries;

namespace ManyEntitiesSender.BPL.Abstraction
{
    public interface IMDMSender
    {
        public void Send(string message, MessageHeaderFromMES header = MessageHeaderFromMES.NotSpecified);
        void Send(object obj, MessageHeaderFromMES header = MessageHeaderFromMES.NotSpecified);
    }
}
