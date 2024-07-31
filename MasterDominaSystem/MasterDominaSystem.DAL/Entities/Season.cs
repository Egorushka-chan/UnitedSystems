using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MasterDominaSystem.DAL.Interfaces;

namespace MasterDominaSystem.DAL.Entities
{
    public partial class Season : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Set> Sets { get; set; } = [];
    }
}
