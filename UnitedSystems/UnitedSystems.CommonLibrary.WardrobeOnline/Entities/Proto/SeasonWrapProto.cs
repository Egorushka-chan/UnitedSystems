using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class SeasonWrapProto(SeasonProto proto) : EntityProto<SeasonS>(proto)
    {
        private Season CreateDB()
        {
            return new() {
                ID = Value.ID,
                Name = Value.Name
            };
        }
        internal override EntityDB GeneralConvertToDB(EntityDB entityDB) => CreateDB();

        internal override EntityDB<SeasonS> GenericConvertToDB(EntityDB<SeasonS> entityDB) => CreateDB();
    }
}
