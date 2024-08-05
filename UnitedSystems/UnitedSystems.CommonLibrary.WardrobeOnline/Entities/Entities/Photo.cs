using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities
{
    public partial class Photo : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string HashCode { get; set; }
        public bool IsDBStored { get; set; } // False по дефолту
        public byte[]? Value { get; set; }  // Может быть пригодится
        [Required, ForeignKey("ClothForeignKey")]
        public int ClothID { get; set; }
        public virtual Cloth? Cloth { get; set; }
    }
}
