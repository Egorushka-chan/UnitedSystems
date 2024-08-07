using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO
{
    public class PersonDTO : EntityDTO<PersonS>
    {
        public int ID { get; set; }
        public string? Name { get; init; }
        public string? Type { get; init; }
        public IReadOnlyList<int>? PhysiqueIDs { get; init; }

        internal override EntityDB GeneralConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityProto GeneralConvertToProto()
        {
            throw new NotImplementedException();
        }

        internal override EntityDB<PersonS> GenericConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityProto<PersonS> GenericConvertToProto()
        {
            throw new NotImplementedException();
        }
    }
}
