using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using System.Linq;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB
{
    public partial class Set : EntityDB<SetS>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }
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

            if (Season != null) {
                season = Season.Name;
            }

            if(SetHasClothes.Count > 0) {
                clothIDs ??= [];
                clothIDs.AddRange(from clothSet in SetHasClothes
                                  select clothSet.ClothID);
            }

            return new() {
                ID = ID,
                Description = Description,
                Name = Name,
                Season = season,
                ClothIDs = clothIDs,
                PhysiqueID = PhysiqueID
            };
        }

        private SetWrapProto CreateProto()
        {
            return new(new() {
                ID = ID,
                Name = Name,
                Description = Description,
                PhysiqueID = PhysiqueID,
                SeasonID = SeasonID
            });
        }
        internal override EntityDTO GeneralConvertToDTO(EntityDTO entityDTO) => CreateDTO();

        internal override EntityProto GeneralConvertToProto(EntityProto entityProto) => CreateProto();

        internal override EntityDTO<SetS> GenericConvertToDTO(EntityDTO<SetS> entityDTO) => CreateDTO();

        internal override EntityProto<SetS> GenericConvertToProto(EntityProto<SetS> entityProto) => CreateProto();
    }
}
