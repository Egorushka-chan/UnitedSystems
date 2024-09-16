using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class SetHasClothesWrapProto(SetHasClothesProto proto) : EntityProto<SetHasClothesS>(proto)
    {
        private SetHasClothes CreateDB()
        {
            return new() {
                ID = Value.ID,
                ClothID = Value.ClothID,
                SetID = Value.SetID
            };
        }

        public static implicit operator SetHasClothes(SetHasClothesWrapProto wrap) => wrap.CreateDB();
        public static implicit operator SetHasClothesProto(SetHasClothesWrapProto wrap) => wrap.Value;
        internal override EntityDB GeneralConvertToDB(EntityDB entityDB) => CreateDB();

        internal override EntityDB<SetHasClothesS> GenericConvertToDB(EntityDB<SetHasClothesS> entityDB) => CreateDB();
    }
}
