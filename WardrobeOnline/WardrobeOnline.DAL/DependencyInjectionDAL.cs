using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using WardrobeOnline.DAL.Interfaces;
using WardrobeOnline.DAL.Repositories;
using WardrobeOnline.DAL.Repositories.Interfaces;

namespace WardrobeOnline.DAL
{
    public static class DependencyInjectionDAL
    {
        public static void AddDataLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IWardrobeContext, WardrobeContext>(opt => opt.UseNpgsql(connectionString), contextLifetime: ServiceLifetime.Scoped);
            //services.AddTransient<IWardrobeContext, WardrobeContext>();
            //services.ConfigureEntity<Person>();
            //services.ConfigureEntity<Cloth>();
            //services.ConfigureEntity<ClothHasMaterials>();
            //services.ConfigureEntity<Physique>();
            //services.ConfigureEntity<Material>();
            //services.ConfigureEntity<Photo>();
            //services.ConfigureEntity<Season>();
            //services.ConfigureEntity<Set>();
            //services.ConfigureEntity<SetHasClothes>();
        }

        private static void ConfigureEntity<T>(this IServiceCollection services) where T : class, IEntityDB
        {
            services.AddTransient<IRepository<T>, Repository<T>>();
        }
    }
}
