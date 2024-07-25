using ManyEntitiesSender.DAL.Implementations;

using Microsoft.EntityFrameworkCore;

using UnitedSystems.CommonLibrary.Models.ManyEntitiesSender;
using UnitedSystems.CommonLibrary.Models.ManyEntitiesSender.Interfaces;

namespace ManyEntitiesSender.DAL.Interfaces
{
    public interface IPackageContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> DbSet<TEntity>() where TEntity : class, IEntity;
        PackageContext Context { get; }
        DbSet<Body> Body { get; set; }
        DbSet<Hand> Hands { get; set; }
        DbSet<Leg> Legs { get; set; }
    }
}
