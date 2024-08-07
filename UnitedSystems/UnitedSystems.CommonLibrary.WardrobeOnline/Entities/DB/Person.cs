using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB
{
    [Table("Person")]
    public partial class Person : EntityDB<PersonS>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Type { get; set; } // Не только люди могут одеваться
        public virtual ICollection<Physique> Physiques { get; set; } = [];

        private PersonDTO CreateDTO()
        {
            List<int>? physiqueIDs = null;

            if(Physiques.Count != 0) {
                foreach(var physique in Physiques) {
                    physiqueIDs ??= [];
                    physiqueIDs.Add(physique.ID);
                }
            }

            return new() {
                ID = ID,
                Name = Name,
                Type = Type,
                PhysiqueIDs = physiqueIDs
            };
        }
        private PersonWrapProto CreateProto()
        {
            return new(new() {
                ID = ID,
                Name = Name,
                Type = Type
            });
        }
        internal override EntityDTO GeneralConvertToDTO() => CreateDTO();
        internal override EntityProto GeneralConvertToProto() => CreateProto();
        internal override EntityDTO<PersonS> GenericConvertToDTO() => CreateDTO();
        internal override EntityProto<PersonS> GenericConvertToProto() => CreateProto();
    }
}
