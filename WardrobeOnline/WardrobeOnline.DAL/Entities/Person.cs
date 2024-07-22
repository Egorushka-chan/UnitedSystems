using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.DAL.Entities
{
    public partial class Person : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Type {get; set; } // Не только люди могут одеваться
        public virtual ICollection<Physique> Physiques { get; set; } = new List<Physique>();
        
    }
}
