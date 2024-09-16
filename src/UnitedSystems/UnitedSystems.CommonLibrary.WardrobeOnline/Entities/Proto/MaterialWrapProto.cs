using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class MaterialWrapProto(MaterialProto proto) : EntityProto<MaterialsS>(proto)
    {
        private Material CreateDB()
        {
            return new() {
                ID = Value.ID,
                Name = Value.Name,
                Description = Value.Description
            };
        }

        public static implicit operator Material(MaterialWrapProto wrap) => wrap.CreateDB();
        public static implicit operator MaterialProto(MaterialWrapProto wrap) => wrap.Value;
        internal override EntityDB GeneralConvertToDB(EntityDB entityDB) => CreateDB();

        internal override EntityDB<MaterialsS> GenericConvertToDB(EntityDB<MaterialsS> entityDB) => CreateDB();
    }
}
