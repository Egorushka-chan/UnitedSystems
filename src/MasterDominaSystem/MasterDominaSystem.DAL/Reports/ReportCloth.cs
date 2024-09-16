using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.DAL.Reports
{
    [Index(nameof(ClothID), nameof(MaterialID), nameof(PhotoID))]
    [Report(typeof(Cloth), typeof(Material), typeof(Photo))]
    public class ReportCloth
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GeneralID { get; set; }
        // Cloth
        public int ClothID { get; set; }
        public string ClothName { get; set; } = string.Empty;
        public string? ClothDescription { get; set; }
        public int ClothRating { get; set; }
        public string ClothSize { get; set; } = string.Empty;
        // Material
        public int? MaterialID { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialDescription { get; set; }
        // Photo
        public int? PhotoID { get; set; }
        public string? PhotoName { get; set; }
        public string? PhotoHashCode { get; set; }
    }
}
