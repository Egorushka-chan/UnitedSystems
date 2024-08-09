using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB
{
    public partial class Season : EntityDB<SeasonS>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Set> Sets { get; set; } = [];

        private SeasonWrapProto CreateProto()
        {
            return new SeasonWrapProto(new WOSenderDB.SeasonProto() {
                ID = ID,
                Name = Name
            });
        }

        internal override EntityProto<SeasonS> GenericConvertToProto(EntityProto<SeasonS> entityProto) => CreateProto();
        internal override EntityProto GeneralConvertToProto(EntityProto entityProto) => CreateProto();
        internal override EntityDTO<SeasonS> GenericConvertToDTO(EntityDTO<SeasonS> entityDTO)
        {
            throw new NotImplementedException();
        }
        internal override EntityDTO GeneralConvertToDTO(EntityDTO entityDTO)
        {
            throw new NotImplementedException();
        }
    }
}
