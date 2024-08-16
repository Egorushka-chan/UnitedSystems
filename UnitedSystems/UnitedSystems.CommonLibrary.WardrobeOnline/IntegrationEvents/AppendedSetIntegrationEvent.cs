using UnitedSystems.CommonLibrary.Messages;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents
{
    public class AppendedSetIntegrationEvent(Set set) : IntegrationEvent
    {
        public Set Set { get; private set; } = set;
        public Season? Season { get; set; } = set.Season ?? throw new ArgumentException("Season not set. Assert Season to Set before", nameof(set.Season));
        public ICollection<SetHasClothes> SetHasClothes { get; set; } = [];
    }
}
