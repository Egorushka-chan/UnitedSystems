using Microsoft.Extensions.DependencyInjection;

using UnitedSystems.CommonLibrary.Models.WardrobeOnline;
using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Interfaces;

using WardrobeOnline.BLL.Models.Settings;
using WardrobeOnline.BLL.Services.Implementations;
using WardrobeOnline.BLL.Services.Implementations.CRUD;
using WardrobeOnline.BLL.Services.Implementations.Pagination;
using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Entities;
using WardrobeOnline.WebApi.Settings;

namespace WardrobeOnline.BLL
{
    public static class DependencyInjectionBLL
    {
        public static void AddBusinessLayer(this IServiceCollection services, ImageSetting imageSetting, RedisSetting redisSetting)
        {
            services.AddTransient<ICRUDProvider<ClothDTO>, ClothProvider>();
            services.AddTransient<ICRUDProvider<PhysiqueDTO>, PhysiqueProvider>();
            services.AddTransient<ICRUDProvider<SetDTO>, SetProvider>();
            services.AddTransient<ICRUDProvider<PersonDTO>, PersonProvider>();

            services.ConfigureValidationLayer<ClothDTO>();
            services.ConfigureValidationLayer<PhysiqueDTO>();
            services.ConfigureValidationLayer<SetDTO>();
            services.ConfigureValidationLayer<PersonDTO>();

            services.AddTransient<IPaginationService<Person>, GeneralPersonPageService>();
            services.AddTransient<IPaginationService<Set>, GeneralPageService<Set>>();
            services.AddTransient<IPaginationService<Physique>, GeneralPageService<Physique>>();
            services.AddTransient<IPaginationService<Cloth>, GeneralPageService<Cloth>>();

            if(imageSetting.Type == "web")
            {
                services.AddSingleton<IImageProvider, WebImageProvider>();
            }
            else
            {
                services.AddSingleton<IImageProvider, LocalImageProvider>();
            }

            services.AddTransient<ICastHelper, CastHelper>();

            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = redisSetting.Configuration;
                opt.InstanceName =  redisSetting.InstanceName;
            });

            services.AddScoped<IGeneralInfoProvider, GeneralInfoProvider>();
            
        }

        private static void ConfigureValidationLayer<TEntityDTO>(this IServiceCollection services) where TEntityDTO : class, IEntityDTO
        {
            services.AddTransient<IValidationLayer<TEntityDTO>, ValidationLayer<TEntityDTO>>();
        }
    }

}
