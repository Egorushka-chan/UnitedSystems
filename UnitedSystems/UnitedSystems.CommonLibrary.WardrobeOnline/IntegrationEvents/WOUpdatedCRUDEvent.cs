using UnitedSystems.CommonLibrary.Messages;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents
{
    public class WOUpdatedCRUDEvent<TEntity> : IntegrationEvent
        where TEntity : IEntity
    {
        public TEntity[] Entities { get; set; } = [];
    }
}
