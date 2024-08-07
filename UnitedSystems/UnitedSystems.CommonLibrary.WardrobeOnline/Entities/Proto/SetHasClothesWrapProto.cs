using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class SetHasClothesWrapProto : EntityProto<SetHasClothesS, SetHasClothesProto>
    {
        public SetHasClothesWrapProto()
        {
        }

        public SetHasClothesWrapProto(SetHasClothesProto proto) : base(proto)
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

        internal override EntityDB<SetHasClothesS> GenericConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityDTO<SetHasClothesS> GenericConvertToDTO()
        {
            throw new NotImplementedException();
        }
    }
}
