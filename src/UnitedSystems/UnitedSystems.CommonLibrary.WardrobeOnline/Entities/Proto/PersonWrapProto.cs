using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class PersonWrapProto(PersonProto proto) : EntityProto<PersonS>(proto)
    {
        private Person CreateDB()
        {
            return new() {
                ID = Value.ID,
                Name = Value.Name,
                Type = Value.Type
            };
        }

        public static implicit operator Person(PersonWrapProto wrap) => wrap.CreateDB();
        public static implicit operator PersonProto(PersonWrapProto wrap) => wrap.Value;
        internal override EntityDB GeneralConvertToDB(EntityDB entityDB) => CreateDB();

        internal override EntityDB<PersonS> GenericConvertToDB(EntityDB<PersonS> entityDB) => CreateDB();
    }
}
