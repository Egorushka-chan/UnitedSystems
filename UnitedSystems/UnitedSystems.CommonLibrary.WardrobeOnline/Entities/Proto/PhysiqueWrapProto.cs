using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class PhysiqueWrapProto : EntityProto<PhysiqueS, PhysiqueProto>
    {
        public PhysiqueWrapProto() : base()
        {
        }
        public PhysiqueWrapProto(PhysiqueProto proto) : base(proto)
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
        internal override EntityDB<PhysiqueS> GenericConvertToDB()
        {
            throw new NotImplementedException();
        }
        internal override EntityDTO<PhysiqueS> GenericConvertToDTO()
        {
            throw new NotImplementedException();
        }
    }
}
