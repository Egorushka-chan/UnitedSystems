using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class SetWrapProto(SetProto proto) : EntityProto<SetS>(proto)
    {
        private Set CreateDB()
        {
            return new() {
                ID = Value.ID,
                Name = Value.Name,
                Description = Value.Description,
                SeasonID = Value.SeasonID,
                PhysiqueID = Value.PhysiqueID
            };
        }

        public static implicit operator Set(SetWrapProto wrap) => wrap.CreateDB();
        public static implicit operator SetProto(SetWrapProto wrap) => wrap.Value;
        internal override EntityDB GeneralConvertToDB(EntityDB entityDB) => CreateDB();

        internal override EntityDB<SetS> GenericConvertToDB(EntityDB<SetS> entityDB) => CreateDB();
    }
}
