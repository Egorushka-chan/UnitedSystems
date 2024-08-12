using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using WardrobeOnline.BLL.Models.Settings;
using WardrobeOnline.BLL.Services.Implementations;
using WardrobeOnline.BLL.Services.Implementations.CRUD;
using WardrobeOnline.BLL.Services.Implementations.Pagination;
using WardrobeOnline.BLL.Services.Implementations.Validation;
using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.WebApi.Settings;

namespace WardrobeOnline.BLL
{
    public static class DependencyInjectionBLL
    {
        public static void AddBusinessLayer(this IServiceCollection services, RedisSetting redisSetting)
        {
            services.AddTransient<ICRUDProvider<ClothDTO>, ClothProvider>();
            services.AddTransient<ICRUDProvider<PhysiqueDTO>, PhysiqueProvider>();
            services.AddTransient<ICRUDProvider<SetDTO>, SetProvider>();
            services.AddTransient<ICRUDProvider<PersonDTO>, PersonProvider>();

            services.ConfigureValidationLayer<ClothDTO>();
            services.ConfigureValidationLayer<PhysiqueDTO>();
            services.ConfigureValidationLayer<SetDTO>();
            services.ConfigureValidationLayer<PersonDTO>();

            services.AddTransient<IPaginationService<Person>, PersonPageService>();
            services.AddTransient<IPaginationService<Set>, GeneralPageService<Set>>();
            services.AddTransient<IPaginationService<Physique>, GeneralPageService<Physique>>();
            services.AddTransient<IPaginationService<Cloth>, GeneralPageService<Cloth>>();

            if (false) {
                services.AddSingleton<IImageProvider, WebImageProvider>();
            }
            else {
                services.AddSingleton<IImageProvider, LocalImageProvider>();
            }

            services.AddTransient<ICastHelper, CastHelper>();

            services.AddStackExchangeRedisCache(opt =>
            {
                
                opt.Configuration = redisSetting.Configuration;
                opt.InstanceName =  redisSetting.InstanceName;
            });

            services.AddScoped<IGeneralInfoProvider, GeneralInfoProvider>();

            services.AddTransient<IValidator<IEntityDTO>, DTOValidator>();
        }

        private static void ConfigureValidationLayer<TEntityDTO>(this IServiceCollection services) where TEntityDTO : class, IEntityDTO
        {
            services.AddTransient<IWrapperCRUDLayer<TEntityDTO>, ValidationLayer<TEntityDTO>>();
        }
    }

}
