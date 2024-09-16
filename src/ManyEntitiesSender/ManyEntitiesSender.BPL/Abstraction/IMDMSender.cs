using UnitedSystems.CommonLibrary.Models.ManyEntitiesSender.Messages.Headers;
using UnitedSystems.CommonLibrary.Queries;

namespace ManyEntitiesSender.BPL.Abstraction
{
    [Obsolete("Необходимо использовать библиотеку UnitedSystems.EventBus, это работать не будет")]
    public interface IMDMSender
    {
        [Obsolete("Необходимо использовать библиотеку UnitedSystems.EventBus, это работать не будет")]
        public void Send(string message, MessageHeaderFromMES header = MessageHeaderFromMES.NotSpecified);
        [Obsolete("Необходимо использовать библиотеку UnitedSystems.EventBus, это работать не будет")]
        void Send(object obj, MessageHeaderFromMES header = MessageHeaderFromMES.NotSpecified);
    }
}
