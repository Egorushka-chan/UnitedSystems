using MasterDominaSystem.DAL.Reports;

using Microsoft.EntityFrameworkCore;

namespace MasterDominaSystem.DAL
{
    public class WardrobeContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ReportCloth> ReportCloths { get; set; }
        public DbSet<ReportGeneral> ReportGenerals { get; set; }
    }
}
