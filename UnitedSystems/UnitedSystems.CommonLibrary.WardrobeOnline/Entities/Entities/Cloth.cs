using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities
{
    public partial class Cloth : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? Rating { get; set; }
        public string? Size { get; set; }

        public virtual ICollection<ClothHasMaterials> ClothHasMaterials { get; set; } = [];
        public virtual ICollection<Photo> Photos { get; set; } = [];
        public virtual ICollection<SetHasClothes> SetClothes { get; set; } = [];
    }
}
