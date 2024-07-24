using UnitedSystems.CommonLibrary.Models.Messages;

namespace UnitedSystems.CommonLibrary.Models.ManyEntitiesSender.Messages.Produced
{
    public class GetMESInfo : BrokerInfoObject
    {
        public int StatusCode {  get; set; }
    }
}
