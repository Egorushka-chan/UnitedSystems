using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class PhotoWrapProto(PhotoProto proto) : EntityProto<PhotoS>(proto)
    {
        private Photo CreateDB()
        {
            return new() {
                ID = Value.ID,
                ClothID = Value.ClothID,
                Name = Value.Name,
                HashCode = Value.HashCode,
                IsDBStored = Value.IsDBStored
            };
        }

        public static implicit operator Photo(PhotoWrapProto wrap) => wrap.CreateDB();
        public static implicit operator PhotoProto(PhotoWrapProto wrap) => wrap.Value;
        internal override EntityDB GeneralConvertToDB(EntityDB entityDB) => CreateDB();

        internal override EntityDB<PhotoS> GenericConvertToDB(EntityDB<PhotoS> entityDB) => CreateDB();
    }
}
