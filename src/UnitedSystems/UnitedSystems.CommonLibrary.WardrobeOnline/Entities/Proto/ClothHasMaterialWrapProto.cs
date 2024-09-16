using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class ClothHasMaterialWrapProto(ClothHasMaterialsProto proto) : EntityProto<ClothHasMaterialsS>(proto)
    {
        private ClothHasMaterials CreateDB()
        {
            return new() {
                ID = Value.ID,
                ClothID = Value.ClothID,
                MaterialID = Value.MaterialID
            };
        }

        public static implicit operator ClothHasMaterials(ClothHasMaterialWrapProto wrap) => wrap.CreateDB();
        public static implicit operator ClothHasMaterialsProto(ClothHasMaterialWrapProto wrap) => wrap.Value;
        internal override EntityDB GeneralConvertToDB(EntityDB entityDB) => CreateDB();
        internal override EntityDB<ClothHasMaterialsS> GenericConvertToDB(EntityDB<ClothHasMaterialsS> entityDB) => CreateDB();
    }
}
