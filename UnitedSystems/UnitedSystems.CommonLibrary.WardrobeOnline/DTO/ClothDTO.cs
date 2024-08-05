using UnitedSystems.CommonLibrary.WardrobeOnline.DTO.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.DTO
{
    public record ClothDTO : IEntityDTO
    {
        public int ID { get; set; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public int? Rating { get; init; }
        public string? Size { get; init; }
        public IReadOnlyList<string>? Materials { get; init; }
        public IReadOnlyList<string>? PhotoPaths { get; init; }
    }
}
