using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class ClothHasMaterialWrapProto : EntityProto<ClothHasMaterialsS, ClothHasMaterialsProto>
    {
        public ClothHasMaterialWrapProto() : base()
        {
        }
        public ClothHasMaterialWrapProto(ClothHasMaterialsProto proto) : base(proto)
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

        internal override EntityDB<ClothHasMaterialsS> GenericConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityDTO<ClothHasMaterialsS> GenericConvertToDTO()
        {
            throw new NotImplementedException();
        }
    }
}
