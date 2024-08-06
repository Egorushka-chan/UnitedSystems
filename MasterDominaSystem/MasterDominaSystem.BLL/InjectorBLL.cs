using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.BLL.Services.Implementations;
using MasterDominaSystem.BLL.Services.Strategies;
using MasterDominaSystem.BLL.Services.Strategies.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities;

namespace MasterDominaSystem.BLL
{
    public static class InjectorBLL
    {
        public static IServiceCollection InjectBLL(this IServiceCollection services)
        {
            services.AddSingleton<ISessionInfoProvider, SessionInfoProvider>()
                .AddDenormalizationStrategies()
                .AddScoped<IDatabaseDenormalizer, DatabaseDenormalizer>();
            return services;
        }

        private static IServiceCollection AddDenormalizationStrategies(this IServiceCollection services)
        {
            services.AddKeyedTransient<IEntityDenormalizer<Person>, PersonDenormalizer>(typeof(Person));
            services.AddKeyedTransient<IEntityDenormalizer<Physique>, PhysiqueDenormalizer>(typeof(Physique));
            services.AddKeyedTransient<IEntityDenormalizer<Set>, SetDenormalizer>(typeof(Set));
            services.AddKeyedTransient<IEntityDenormalizer<Season>, SeasonDenormalizer>(typeof(Season));
            services.AddKeyedTransient<IEntityDenormalizer<Cloth>, ClothDenormalizer>(typeof(Cloth));
            services.AddKeyedTransient<IEntityDenormalizer<SetHasClothes>, SetHasClothesDenormalizer>(typeof(SetHasClothes));
            services.AddKeyedTransient<IEntityDenormalizer<Photo>, PhotoDenormalizer>(typeof(Photo));
            services.AddKeyedTransient<IEntityDenormalizer<Material>, MaterialDenormalizer>(typeof(Material));
            services.AddKeyedTransient<IEntityDenormalizer<ClothHasMaterials>, ClothHasMaterialsDenormalizer>(typeof(ClothHasMaterials));

            return services;
        }
    }
}
