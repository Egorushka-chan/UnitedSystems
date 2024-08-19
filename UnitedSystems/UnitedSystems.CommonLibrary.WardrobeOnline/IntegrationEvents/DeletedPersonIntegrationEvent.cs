using UnitedSystems.CommonLibrary.Messages;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents
{
    public class DeletedPersonIntegrationEvent : IntegrationEvent
    {
        public int DeletedID { get; set; }
    }
}
