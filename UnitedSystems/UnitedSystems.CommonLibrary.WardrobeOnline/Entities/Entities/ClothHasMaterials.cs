using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities
{
    public partial class ClothHasMaterials : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID {get; set;}
        [Required, ForeignKey("ClothForeignKey")]
        public int ClothID { get; set;}
        [Required, ForeignKey("MaterialForeignKey")]
        public int MaterialID { get; set;}
        public virtual Cloth? Cloth { get; set;}
        public virtual Material? Material { get; set;}
    }
}
