using UnitedSystems.CommonLibrary.Models.Messages;

namespace UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages
{
    public class DeleteWOInfo : BrokerInfoObject
    {
        public string RequestQuery { get; set; } = "NaN";
        public int StatusCode { get; set; }
        public ErrorResponse? ErrorResponse { get; set; }
    }
}
