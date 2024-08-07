using UnitedSystems.CommonLibrary.Messages;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents
{
    public class WODeletedCRUDEvent<TEntity> : IntegrationEvent
        where TEntity : IEntityDB
    {
        public int[] EntitiesIDs { get; set; } = [];
    }
}
