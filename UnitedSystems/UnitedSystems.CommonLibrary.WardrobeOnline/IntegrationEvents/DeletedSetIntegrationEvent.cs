using UnitedSystems.CommonLibrary.Messages;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents
{
    [Obsolete("Бесполезный класс, в текущей бизнес логике, его функционал спокойно заменяет WODeletedCRUDEvent")]
    public class DeletedSetIntegrationEvent : IntegrationEvent
    {
        public int DeletedID { get; set; }
    }
}
