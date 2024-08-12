using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDominaSystem.DAL.Reports
{
    public class ReportPerson
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GeneralID { get; set; }
        // Person
        public int PersonID { get; set; }
        public string PersonName { get; set; } = string.Empty;
        public string PersonType { get; set; } = string.Empty; // Не только люди могут одеваться
        // Physique
        public int? PhysiqueID { get; set; }
        public int? PhysiqueWeight { get; set; }
        public int? PhysiqueGrowth { get; set; }
        public int? PhysiqueForce { get; set; }
        public string? PhysiqueDescription { get; set; }
        // Set
        public int? SetID { get; set; }
        public string? SetName { get; set; }
        public string? SetDescription { get; set; }
        // Season
        public int? SeasonID { get; set; }
        public string? SeasonName { get; set; }
        // Cloth
        public int? ClothID { get; set; }
        public string? ClothName { get; set; }
        public string? ClothDescription { get; set; }
        public int? ClothRating { get; set; }
        public string? ClothSize { get; set; }
        // Photo 
        public int? PhotoID { get; set; }
        public string? PhotoName { get; set; }
        public string? PhotoHashCode { get; set; }
    }
}
