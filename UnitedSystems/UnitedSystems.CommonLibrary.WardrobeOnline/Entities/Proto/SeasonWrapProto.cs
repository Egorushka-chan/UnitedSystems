using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class SeasonWrapProto : EntityProto<SeasonS, SeasonProto>
    {
        public SeasonWrapProto()
        {
        }

        public SeasonWrapProto(SeasonProto proto) : base(proto)
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

        internal override EntityDB<SeasonS> GenericConvertToDB()
        {
            throw new NotImplementedException();
        }

        internal override EntityDTO<SeasonS> GenericConvertToDTO()
        {
            throw new NotImplementedException();
        }
    }
}
