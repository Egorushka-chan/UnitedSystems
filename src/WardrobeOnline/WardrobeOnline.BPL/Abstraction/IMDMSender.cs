using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages.Headers;

namespace WardrobeOnline.BPL.Abstractions
{
    public interface IMDMSender
    {
        public void Send(string message, MessageHeaderFromWO header = MessageHeaderFromWO.NotSpecified);
        void Send(object obj, MessageHeaderFromWO header = MessageHeaderFromWO.NotSpecified);
    }
}
