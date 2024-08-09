using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB
{
    [Table("Cloth")]
    public partial class Cloth : EntityDB<ClothS>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? Rating { get; set; }
        public string? Size { get; set; }

        public virtual ICollection<ClothHasMaterials> ClothHasMaterials { get; set; } = [];
        public virtual ICollection<Photo> Photos { get; set; } = [];
        public virtual ICollection<SetHasClothes> SetClothes { get; set; } = [];


        private ClothDTO CreateDTO()
        {
            List<string>? materials = null;
            List<string>? photoPaths = null;

            if (ClothHasMaterials.Count != 0) {
                foreach(ClothHasMaterials chm in ClothHasMaterials) {
                    if(chm.Cloth != null) {
                        materials ??= [];
                        materials.Add(chm.Cloth.Name);
                    }
                }
            }

            if(Photos.Count != 0) {
                foreach(Photo photo in Photos) {
                    photoPaths ??= [];
                    photoPaths.Add(photo.HashCode);
                }
            }

            return new ClothDTO() {
                ID = ID,
                Name = Name,
                Description = Description,
                Rating = Rating,
                Size = Size,
                Materials = materials,
                PhotoPaths = photoPaths
            };
        }

        private ClothWrapProto CreateProto()
        {
            return new ClothWrapProto(
                new WOSenderDB.ClothProto() {
                    ID = ID,
                    Name = Name,
                    Description = Description,
                    Rating = Rating,
                    Size = Size
                });
        }

        internal override EntityDTO<ClothS> GenericConvertToDTO(EntityDTO<ClothS> entityDTO) => CreateDTO();

        internal override EntityProto<ClothS> GenericConvertToProto(EntityProto<ClothS> entityProto) => CreateProto();

        internal override EntityDTO GeneralConvertToDTO(EntityDTO entityDTO) => CreateDTO();

        internal override EntityProto GeneralConvertToProto(EntityProto entityProto) => CreateProto();
    }
}
