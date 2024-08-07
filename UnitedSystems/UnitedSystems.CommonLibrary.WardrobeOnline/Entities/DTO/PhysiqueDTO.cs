using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO
{
    public class PhysiqueDTO : EntityDTO<PhysiqueS>
    {
        public int ID { get; set; }
        public string? Description { get; init; }
        public int? Growth { get; init; }
        public int? Weight { get; init; }
        public int? Force { get; init; }
        public int? PersonID { get; init; }
        public IReadOnlyList<int>? SetIDs { get; init; }

        internal override EntityDB GeneralConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityProto GeneralConvertToProto()
        {
            throw new NotImplementedException();
        }

        internal override EntityDB<PhysiqueS> GenericConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityProto<PhysiqueS> GenericConvertToProto()
        {
            throw new NotImplementedException();
        }
    }
}
