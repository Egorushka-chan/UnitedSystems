﻿using MasterDominaSystem.DAL.Reports;

using Microsoft.EntityFrameworkCore;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace MasterDominaSystem.DAL
{
    public class MasterContext : DbContext
    {
        public DbSet<ReportCloth> ReportCloths { get; set; }
        public DbSet<ReportPerson> ReportGenerals { get; set; }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Physique> Physiques { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SetHasClothes> SetHasClothes { get; set; }
        public DbSet<Cloth> Clothes { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ClothHasMaterials> ClothHasMaterials { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public MasterContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }
    }
}