using UnitedSystems.CommonLibrary.Messages;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents
{
    public class DeletedClothIntegrationEvent : IntegrationEvent
    {
        public int DeletedID { get; set; }
    }
}
