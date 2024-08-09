using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB
{
    public partial class Material : EntityDB<MaterialsS>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public virtual ICollection<ClothHasMaterials> ClothHasMaterials { get; set; } = [];

        private MaterialWrapProto CreateProto()
        {
            return new(new() {
                ID = ID,
                Name = Name,
                Description = Description
            });
        }

        internal override EntityProto GeneralConvertToProto(EntityProto entityProto) => CreateProto();
        internal override EntityProto<MaterialsS> GenericConvertToProto(EntityProto<MaterialsS> entityProto) => CreateProto();

        internal override EntityDTO<MaterialsS> GenericConvertToDTO(EntityDTO<MaterialsS> entityDTO)
        {
            throw new NotImplementedException();
        }
        internal override EntityDTO GeneralConvertToDTO(EntityDTO entityDTO)
        {
            throw new NotImplementedException();
        }
    }
}
