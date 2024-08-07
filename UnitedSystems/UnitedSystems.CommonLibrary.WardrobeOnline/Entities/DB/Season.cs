using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Proto;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB
{
    [Table("Season")]
    public partial class Season : EntityDB<SeasonS>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
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

        internal override EntityProto<SeasonS> GenericConvertToProto() => CreateProto();
        internal override EntityProto GeneralConvertToProto() => CreateProto();
        internal override EntityDTO<SeasonS> GenericConvertToDTO()
        {
            throw new NotImplementedException();
        }
        internal override EntityDTO GeneralConvertToDTO()
        {
            throw new NotImplementedException();
        }
    }
}
