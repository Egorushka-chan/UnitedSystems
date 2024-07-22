using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.DAL.Entities
{
    public partial class Material : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<ClothHasMaterials> ClothHasMaterials { get; set; } =  new List<ClothHasMaterials>();
    }
}
