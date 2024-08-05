using Microsoft.EntityFrameworkCore;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace WardrobeOnline.DAL.Interfaces
{
    public interface IWardrobeContext
    {
        DbSet<Cloth> Clothes { get; set; }
        DbSet<ClothHasMaterials> ClothHasMaterials { get; set; }
        DbSet<Physique> Physiques { get; set; }
        DbSet<Material> Materials { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<Photo> Photos { get; set; }
        DbSet<Season> Seasons { get; set; }
        DbSet<SetHasClothes> SetHasClothes { get; set; }
        DbSet<Set> Sets { get; set; }

        WardrobeContext Context();
        DbSet<T> DBSet<T>() where T : class, IEntity;
        Task<int> SaveChangesAsync();
    }
}
