using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto
{
    public class ClothWrapProto(ClothProto clothProto) : EntityProto<ClothS>(clothProto)
    {
        private Cloth CreateDB()
        {
            return new() {
                ID = Value.ID,
                Name = Value.Name,
                Description = Value.Description,
                Rating = Value.Rating,
                Size = Value.Size
            };
        }

        internal override EntityDB GeneralConvertToDB(EntityDB entityDB) => CreateDB();
        internal override EntityDB<ClothS> GenericConvertToDB(EntityDB<ClothS> entityDB) => CreateDB();
    }
}
