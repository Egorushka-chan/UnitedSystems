using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities
{
    public partial class Physique : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int Growth { get; set; }
        public int Force { get;  set;}
        public string? Description { get; set; }
        [Required, ForeignKey("PersonForeignKey")]
        public int PersonID { get; set; }
        public virtual Person? Person { get; set; }
        public virtual ICollection<Set> Sets { get; set; } = [];
    }
}
