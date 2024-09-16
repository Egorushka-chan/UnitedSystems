using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.Extensions.Diagnostics.HealthChecks;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto;

using WOSenderDB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB
{
    public partial class Person : EntityDB<PersonS>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }
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

        public static implicit operator PersonWrapProto(Person entity) => entity.CreateProto();
        public static implicit operator PersonProto(Person entity) => (PersonWrapProto)entity;
        internal override EntityDTO GeneralConvertToDTO(EntityDTO entityDTO) => CreateDTO();
        internal override EntityProto GeneralConvertToProto(EntityProto entityProto) => CreateProto();
        internal override EntityDTO<PersonS> GenericConvertToDTO(EntityDTO<PersonS> entityDTO) => CreateDTO();
        internal override EntityProto<PersonS> GenericConvertToProto(EntityProto<PersonS> entityProto) => CreateProto();
    }
}
