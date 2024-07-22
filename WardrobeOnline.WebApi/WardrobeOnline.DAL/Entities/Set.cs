using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.DAL.Entities
{
    public partial class Set : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public int PhysiqueID { get;set; }
        [Required]
        public int SeasonID { get; set; }
        public virtual ICollection<SetHasClothes> SetHasClothes { get; set; } = new List<SetHasClothes>();
        [ForeignKey("PhysiqueID")]
        public virtual Physique? Physique { get; set; }
        [ForeignKey("SeasonID")]
        public virtual Season? Season { get; set; }
    }
}
