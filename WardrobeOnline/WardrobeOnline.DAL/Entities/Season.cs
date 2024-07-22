using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.DAL.Entities
{
    public partial class Season : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Set> Sets { get; set; } = new List<Set>();
    }
}
