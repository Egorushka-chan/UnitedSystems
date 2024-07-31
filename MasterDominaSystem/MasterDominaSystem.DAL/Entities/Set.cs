using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MasterDominaSystem.DAL.Interfaces;

namespace MasterDominaSystem.DAL.Entities
{
    public partial class Set : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public int PhysiqueID { get;set; }
        [Required]
        public int SeasonID { get; set; }
        public virtual ICollection<SetHasClothes> SetHasClothes { get; set; } = [];
        [ForeignKey("PhysiqueID")]
        public virtual Physique? Physique { get; set; }
        [ForeignKey("SeasonID")]
        public virtual Season? Season { get; set; }
    }
}
