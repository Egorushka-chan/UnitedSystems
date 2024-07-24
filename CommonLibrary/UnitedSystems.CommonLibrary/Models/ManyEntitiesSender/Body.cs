using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using UnitedSystems.CommonLibrary.Models.ManyEntitiesSender.Interfaces;

namespace UnitedSystems.CommonLibrary.Models.ManyEntitiesSender
{
    public partial class Body : IEntity
    {
        [Key]
        [JsonPropertyName("id")]
        public long ID { get; set; }
        [JsonPropertyName("mightiness")]
        public string Mightiness {get; set; }
    }
}
