using UnitedSystems.CommonLibrary.Models.Messages;

namespace UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages
{
    public class ErrorWOInfo : BrokerInfoObject
    {
        public string Header { get; set; }  = "NaN";
        public Dictionary<string, string> Query { get; set; } = [];
        public ErrorResponse ErrorResponse { get; set; } = new ErrorResponse() { Body = "default", Code=500};
    }
}
