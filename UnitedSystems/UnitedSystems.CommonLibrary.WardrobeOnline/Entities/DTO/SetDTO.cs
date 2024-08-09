using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO
{
    public class SetDTO : EntityDTO<SetS>
    {
        public override int ID { get; set; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? Season { get; init; }
        public int? PhysiqueID { get; init; }
        public IReadOnlyList<int>? ClothIDs { get; init; }

        internal override EntityDB GeneralConvertToDB(EntityDB entityDB)
        {
            throw new NotImplementedException();
        }

        internal override EntityDB<SetS> GenericConvertToDB(EntityDB<SetS> entityDB)
        {
            throw new NotImplementedException();
        }
    }
}
