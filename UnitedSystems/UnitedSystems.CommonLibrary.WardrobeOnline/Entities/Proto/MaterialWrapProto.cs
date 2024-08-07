using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class MaterialWrapProto : EntityProto<MaterialsS, MaterialProto>
    {
        public MaterialWrapProto() : base()
        {
        }
        public MaterialWrapProto(MaterialProto proto) : base(proto)
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

        internal override EntityDB<MaterialsS> GenericConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityDTO<MaterialsS> GenericConvertToDTO()
        {
            throw new NotImplementedException();
        }
    }
}
