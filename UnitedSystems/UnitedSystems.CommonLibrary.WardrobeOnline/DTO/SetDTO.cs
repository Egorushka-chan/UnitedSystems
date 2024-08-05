using UnitedSystems.CommonLibrary.WardrobeOnline.DTO.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.DTO
{
    public record SetDTO : IEntityDTO
    {
        public int ID { get; set; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? Season { get; init; }
        public int? PhysiqueID { get; init; }
        public IReadOnlyList<int>? ClothIDs { get; init; }
    }
}
