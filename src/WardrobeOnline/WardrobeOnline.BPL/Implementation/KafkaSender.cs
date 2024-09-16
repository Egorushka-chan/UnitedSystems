using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages.Headers;

using WardrobeOnline.BPL.Abstractions;

namespace WardrobeOnline.BPL.Implementations
{
    public class KafkaSender : IMDMSender
    {
        public void Send(string message, MessageHeaderFromWO header = MessageHeaderFromWO.NotSpecified)
        {
            throw new NotImplementedException();
        }

        public void Send(object obj, MessageHeaderFromWO header = MessageHeaderFromWO.NotSpecified)
        {
            throw new NotImplementedException();
        }
    }
}
