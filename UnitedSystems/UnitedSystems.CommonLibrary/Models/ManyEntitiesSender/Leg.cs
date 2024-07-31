using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using UnitedSystems.CommonLibrary.Models.ManyEntitiesSender.Interfaces;
    
namespace UnitedSystems.CommonLibrary.Models.ManyEntitiesSender
{
    public partial class Leg : IEntity
    {
        [Key]
        [JsonPropertyName("id")]
        public long ID  { get; set; }
        [JsonPropertyName("state")]
        public string State {get; set; }
    }
}
