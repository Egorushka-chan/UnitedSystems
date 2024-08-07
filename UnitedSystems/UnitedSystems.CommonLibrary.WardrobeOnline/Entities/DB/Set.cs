using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB
{
    [Table("Set")]
    public partial class Set : EntityDB<SetS>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public int PhysiqueID { get; set; }
        [Required]
        public int SeasonID { get; set; }
        public virtual ICollection<SetHasClothes> SetHasClothes { get; set; } = [];
        [ForeignKey("PhysiqueID")]
        public virtual Physique? Physique { get; set; }
        [ForeignKey("SeasonID")]
        public virtual Season? Season { get; set; }

        private SetDTO CreateDTO()
        {
            string? season = null;
            List<int>? clothIDs = null;

            return new() {
                ID = ID,
                Description = Description,
                Name = Name,
                Season = season,
                ClothIDs = clothIDs,
                PhysiqueID = PhysiqueID
            };
        }
        internal override EntityDTO GeneralConvertToDTO()
        {
            throw new NotImplementedException();
        }

        internal override EntityProto GeneralConvertToProto()
        {
            throw new NotImplementedException();
        }

        internal override EntityDTO<SetS> GenericConvertToDTO()
        {
            throw new NotImplementedException();
        }

        internal override EntityProto<SetS> GenericConvertToProto()
        {
            throw new NotImplementedException();
        }
    }
}
