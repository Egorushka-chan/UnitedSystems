using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO
{
    public class SetDTO : EntityDTO<SetS>
    {
        public int ID { get; set; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? Season { get; init; }
        public int? PhysiqueID { get; init; }
        public IReadOnlyList<int>? ClothIDs { get; init; }

        internal override EntityDB GeneralConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityProto GeneralConvertToProto()
        {
            throw new NotImplementedException();
        }

        internal override EntityDB<SetS> GenericConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityProto<SetS> GenericConvertToProto()
        {
            throw new NotImplementedException();
        }
    }
}
