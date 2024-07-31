using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Interfaces;

namespace UnitedSystems.CommonLibrary.Models.WardrobeOnline
{
    public record PhysiqueDTO : IEntityDTO
    {
        public int ID { get; set; }
        public string? Description { get; init; }
        public int? Growth { get; init; }
        public int? Weight { get; init; }
        public int? Force { get; init; }
        public int? PersonID { get; init; }
        public IReadOnlyList<int>? SetIDs { get; init; }
    }
}
