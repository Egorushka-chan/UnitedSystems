using UnitedSystems.CommonLibrary.Models.Messages;
using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Interfaces;

namespace UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages
{
    public class GetWOInfo : BrokerInfoObject
    {
        public Dictionary<string, string> RequestQuery { get; set; } = [];
        public int StatusCode { get; set; }
        public IEntityDTO? ObjectDTO { get; set; }
    }
}
