using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class PhotoWrapProto : EntityProto<PhotoS, PhotoProto>
    {
        public PhotoWrapProto() : base()
        {
        }
        public PhotoWrapProto(PhotoProto proto) : base(proto)
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

        internal override EntityDB<PhotoS> GenericConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityDTO<PhotoS> GenericConvertToDTO()
        {
            throw new NotImplementedException();
        }
    }
}
