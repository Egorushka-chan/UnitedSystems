using MasterDominaSystem.DAL.Reports;

using Microsoft.EntityFrameworkCore;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.DAL
{
    public class MasterContext : DbContext
    {
        public DbSet<ReportCloth> ReportCloths { get; set; }
        public DbSet<ReportPerson> ReportGenerals { get; set; }

        public MasterContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }
    }
}
