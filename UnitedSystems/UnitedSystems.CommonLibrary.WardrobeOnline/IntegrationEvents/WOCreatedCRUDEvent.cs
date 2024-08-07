using UnitedSystems.CommonLibrary.Messages;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents
{
    public class WOCreatedCRUDEvent<TEntity> : IntegrationEvent
        where TEntity : IEntityDB
    {
        public TEntity[] Entities { get; set; } = [];
    }
}
