using UnitedSystems.CommonLibrary.Messages;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents
{
    public class AppendedClothIntegrationEvent(Cloth cloth) : IntegrationEvent
    {
        public Cloth Cloth { get; private set; } = cloth;
        public ICollection<ClothHasMaterials> ClothHasMaterials { get; set; } = [];
        public ICollection<Material> Materials { get; set; } = [];
        public ICollection<Photo> Photos { get; set; } = [];
    }
}
