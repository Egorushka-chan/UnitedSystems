using UnitedSystems.CommonLibrary.Models.Messages;
using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Interfaces;

namespace UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages
{
    public class GetWOInfo : BrokerInfoObject
    {
        public string RequestQuery { get; set; } = "NaN";
        public int StatusCode { get; set; }
        public ErrorResponse? ErrorResponse { get; set; }
        public IEntityDTO? ReturnedDTO { get; set; }
    }
}
