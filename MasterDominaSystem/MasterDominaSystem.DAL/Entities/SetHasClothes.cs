using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MasterDominaSystem.DAL.Interfaces;

namespace MasterDominaSystem.DAL.Entities
{
    public partial class SetHasClothes : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, ForeignKey("ClothForeignKey")]
        public int ClothID { get; set; }
        [Required, ForeignKey("SetForeignKey")]
        public int SetID { get; set; }
        public virtual Cloth? Cloth { get; set; }
        public virtual Set? Set { get; set; }
    }
}
