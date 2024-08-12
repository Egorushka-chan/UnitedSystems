using System.Reflection;

using Microsoft.EntityFrameworkCore;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.DAL
{
    public class WardrobeContext : DbContext, IWardrobeContext
    {
        public WardrobeContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        {
            if (this.Database.EnsureCreated()) {
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>()
                .Property(e => e.IsDBStored)
                .HasDefaultValue(false);

            modelBuilder.Entity<Season>()
                .HasMany(season => season.Sets)
                .WithOne(set => set.Season)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<T> DBSet<T>() where T : class, IEntityDB
        {
            return this.Set<T>();
        }

        public WardrobeContext Context() => this;

        public Task<int> SaveChangesAsync()
        {
            var changeTracker = this.ChangeTracker;
            
            return base.SaveChangesAsync();
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Physique> Physiques { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SetHasClothes> SetHasClothes { get; set; }
        public DbSet<Cloth> Clothes { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ClothHasMaterials> ClothHasMaterials { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
