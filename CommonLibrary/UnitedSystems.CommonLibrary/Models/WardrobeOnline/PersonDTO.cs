using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Interfaces;

namespace UnitedSystems.CommonLibrary.Models.WardrobeOnline
{
    public record PersonDTO : IEntityDTO
    {
        public int ID { get; set; }
        public string? Name { get; init; }
        public string? Type { get; init; }
        public IReadOnlyList<int>? PhysiqueIDs { get; init; }
    }
}
