using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB
{
    [Table("Physique")]
    public partial class Physique : EntityDB<PhysiqueS>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int Growth { get; set; }
        public int Force { get; set; }
        public string? Description { get; set; }
        [Required, ForeignKey("PersonForeignKey")]
        public int PersonID { get; set; }
        public virtual Person? Person { get; set; }
        public virtual ICollection<Set> Sets { get; set; } = [];


        private PhysiqueWrapProto CreateProto()
        {
            return new(new() {
                ID = ID,
                Force = Force,
                Growth = Growth,
                Weight = Weight,
                PersonID = PersonID,
                Description = Description
            });
        }
        private PhysiqueDTO CreateDTO()
        {
            List<int>? setIDs = null;
            if(Sets.Count != 0) {
                foreach(Set set in Sets) {
                    setIDs ??= [];
                    setIDs.Add(set.ID);
                }
            }

            return new() {
                ID = ID,
                Weight = Weight,
                Growth = Growth,
                Force = Force,
                Description = Description,
                PersonID = PersonID,
                SetIDs = setIDs
            };
        }
        internal override EntityDTO GeneralConvertToDTO(EntityDTO entityDTO) => CreateDTO();
        internal override EntityProto GeneralConvertToProto(EntityProto entityProto) => CreateProto();
        internal override EntityDTO<PhysiqueS> GenericConvertToDTO(EntityDTO<PhysiqueS> entityDTO) => CreateDTO();
        internal override EntityProto<PhysiqueS> GenericConvertToProto(EntityProto<PhysiqueS> entityProto) => CreateProto();
    }
}
