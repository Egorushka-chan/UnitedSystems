using UnitedSystems.CommonLibrary.Messages;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents
{
    public class DeletedSetIntegrationEvent : IntegrationEvent
    {
        public int DeletedID { get; set; }
    }
}
