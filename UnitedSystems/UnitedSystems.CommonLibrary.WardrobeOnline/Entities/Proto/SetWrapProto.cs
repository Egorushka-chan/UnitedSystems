using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class SetWrapProto : EntityProto<SetS, SetProto>
    {
        public SetWrapProto()
        {
        }

        public SetWrapProto(SetProto proto) : base(proto)
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

        internal override EntityDB<SetS> GenericConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityDTO<SetS> GenericConvertToDTO()
        {
            throw new NotImplementedException();
        }
    }
}
