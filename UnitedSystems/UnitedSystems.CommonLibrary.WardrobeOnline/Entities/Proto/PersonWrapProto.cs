using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class PersonWrapProto : EntityProto<PersonS, PersonProto>
    {
        public PersonWrapProto() : base()
        {
        }
        public PersonWrapProto(PersonProto proto) : base(proto) 
        {
        }

        internal override EntityDB GeneralConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityDTO GeneralConvertToDTO()
        {
            throw new NotImplementedException();
        }

        internal override EntityDB<PersonS> GenericConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityDTO<PersonS> GenericConvertToDTO()
        {
            throw new NotImplementedException();
        }
    }
}
