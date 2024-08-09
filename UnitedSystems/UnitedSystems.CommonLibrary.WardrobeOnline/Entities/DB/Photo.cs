using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB
{
    public partial class Photo : EntityDB<PhotoS>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string HashCode { get; set; } = string.Empty;
        public bool IsDBStored { get; set; } // False по дефолту
        public byte[]? Value { get; set; }  // Может быть пригодится
        [Required, ForeignKey("ClothForeignKey")]
        public int ClothID { get; set; }
        public virtual Cloth? Cloth { get; set; }

        private PhotoWrapProto CreateProto()
        {
            return new(new() {
                ID = ID,
                Name = Name,
                HashCode = HashCode,
                ClothID = ClothID,
                IsDBStored = IsDBStored
            });
        }

        internal override EntityProto GeneralConvertToProto(EntityProto entityProto) => CreateProto();
        internal override EntityProto<PhotoS> GenericConvertToProto(EntityProto<PhotoS> entityProto) => CreateProto();

        internal override EntityDTO GeneralConvertToDTO(EntityDTO entityDTO)
        {
            throw new NotImplementedException();
        }
        internal override EntityDTO<PhotoS> GenericConvertToDTO(EntityDTO<PhotoS> entityDTO)
        {
            throw new NotImplementedException();
        }
    }
}
