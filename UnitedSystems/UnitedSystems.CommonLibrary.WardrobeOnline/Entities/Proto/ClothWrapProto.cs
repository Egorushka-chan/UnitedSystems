using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class ClothWrapProto : EntityProto<ClothS, ClothProto>
    {
        public ClothWrapProto() : base()
        {
        }
        public ClothWrapProto(ClothProto clothProto) : base(clothProto)
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

        internal override EntityDB<ClothS> GenericConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityDTO<ClothS> GenericConvertToDTO()
        {
            throw new NotImplementedException();
        }
    }
}
