using UnitedSystems.CommonLibrary.Models.Messages;

namespace UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages
{
    public class PagedGetWOInfo : BrokerInfoObject
    {
        public Dictionary<string, string> RequestQuery { get; set; } = [];
        public int StatusCode { get; set; }
        public List<int> ResponseIDs { get; set; } = [];
    }
}
