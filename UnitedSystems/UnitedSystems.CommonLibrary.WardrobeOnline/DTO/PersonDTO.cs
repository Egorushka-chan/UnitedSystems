using UnitedSystems.CommonLibrary.WardrobeOnline.DTO.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.DTO
{
    public record PersonDTO : IEntityDTO
    {
        public int ID { get; set; }
        public string? Name { get; init; }
        public string? Type { get; init; }
        public IReadOnlyList<int>? PhysiqueIDs { get; init; }
    }
}
