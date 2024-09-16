using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO
{
    public class PersonDTO : EntityDTO<PersonS>
    {
        public override int ID { get; set; }
        public string? Name { get; init; }
        public string? Type { get; init; }
        public IReadOnlyList<int>? PhysiqueIDs { get; init; }

        internal override EntityDB GeneralConvertToDB(EntityDB entityDB)
        {
            throw new NotImplementedException();
        }

        internal override EntityDB<PersonS> GenericConvertToDB(EntityDB<PersonS> entityDB)
        {
            throw new NotImplementedException();
        }
    }
}
