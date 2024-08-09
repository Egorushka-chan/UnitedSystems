using MasterDominaSystem.DAL.Reports;

using Microsoft.EntityFrameworkCore;

namespace MasterDominaSystem.DAL
{
    public class MasterContext : DbContext
    {
        public DbSet<ReportCloth> ReportCloths { get; set; }
        public DbSet<ReportGeneral> ReportGenerals { get; set; }

        public MasterContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }
    }
}
