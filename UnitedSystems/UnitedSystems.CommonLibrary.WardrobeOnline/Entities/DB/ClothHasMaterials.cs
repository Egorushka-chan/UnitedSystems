using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB
{
    [Table("ClothHasMaterials")]
    public partial class ClothHasMaterials : EntityDB<ClothHasMaterialsS>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, ForeignKey("ClothForeignKey")]
        public int ClothID { get; set; }
        [Required, ForeignKey("MaterialForeignKey")]
        public int MaterialID { get; set; }
        public virtual Cloth? Cloth { get; set; }
        public virtual Material? Material { get; set; }

        private ClothHasMaterialWrapProto CreateProto()
        {
            return new(new ClothHasMaterialsProto() {
                ID = ID,
                ClothID = ClothID,
                MaterialID = MaterialID
            });
        }
        internal override EntityProto<ClothHasMaterialsS> GenericConvertToProto() => CreateProto();
        internal override EntityProto GeneralConvertToProto() => CreateProto();


        internal override EntityDTO GeneralConvertToDTO()
        {
            throw new NotImplementedException();
        }
        internal override EntityDTO<ClothHasMaterialsS> GenericConvertToDTO()
        {
            throw new NotImplementedException();
        }
    }
}
