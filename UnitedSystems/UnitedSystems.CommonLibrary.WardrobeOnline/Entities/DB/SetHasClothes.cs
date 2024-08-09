using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB
{
    [Table("SetHasClothes")]
    public partial class SetHasClothes : EntityDB<SetHasClothesS>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }
        [Required, ForeignKey("ClothForeignKey")]
        public int ClothID { get; set; }
        [Required, ForeignKey("SetForeignKey")]
        public int SetID { get; set; }
        public virtual Cloth? Cloth { get; set; }
        public virtual Set? Set { get; set; }

        private SetHasClothesWrapProto CreateProto()
        {
            return new(new() {
                ID = ID,
                ClothID = ClothID,
                SetID = SetID
            });
        }
        internal override EntityProto GeneralConvertToProto(EntityProto entityProto) => CreateProto();
        internal override EntityProto<SetHasClothesS> GenericConvertToProto(EntityProto<SetHasClothesS> entityProto) => CreateProto();

        internal override EntityDTO GeneralConvertToDTO(EntityDTO entityDTO)
        {
            throw new NotImplementedException();
        }
        internal override EntityDTO<SetHasClothesS> GenericConvertToDTO(EntityDTO<SetHasClothesS> entityDTO)
        {
            throw new NotImplementedException();
        }
    }
}
