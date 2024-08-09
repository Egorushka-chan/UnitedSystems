using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO
{
    public class ClothDTO : EntityDTO<ClothS>
    {
        public override int ID { get; set; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public int? Rating { get; init; }
        public string? Size { get; init; }
        public IReadOnlyList<string>? Materials { get; init; }
        public IReadOnlyList<string>? PhotoPaths { get; init; }

        internal override EntityDB GeneralConvertToDB(EntityDB entityDB)
        {
            throw new NotImplementedException();
        }

        internal override EntityDB<ClothS> GenericConvertToDB(EntityDB<ClothS> entityDB)
        {
            throw new NotImplementedException();
        }
    }
}
