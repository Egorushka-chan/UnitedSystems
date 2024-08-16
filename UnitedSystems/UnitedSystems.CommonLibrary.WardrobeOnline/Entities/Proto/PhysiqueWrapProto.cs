using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class PhysiqueWrapProto(PhysiqueProto proto) : EntityProto<PhysiqueS>(proto)
    {
        private Physique CreateDB()
        {
            return new() {
                ID = Value.ID,
                Description = Value.Description,
                Growth = Value.Growth,
                Weight = Value.Weight,
                Force = Value.Force,
                PersonID = Value.PersonID
            };
        }

        public static implicit operator Physique(PhysiqueWrapProto wrap) => wrap.CreateDB();
        public static implicit operator PhysiqueProto(PhysiqueWrapProto wrap) => wrap.Value;
        internal override EntityDB GeneralConvertToDB(EntityDB entityDB) => CreateDB();
        internal override EntityDB<PhysiqueS> GenericConvertToDB(EntityDB<PhysiqueS> entityDB) => CreateDB();
    }
}
