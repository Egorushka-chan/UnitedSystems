﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MasterDominaSystem.DAL
{
    public static class InjectorDAL
    {
        public static IServiceCollection InjectDAL(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WardrobeContext>(opt => {
                opt.UseNpgsql(connectionString);
            });
            return services;
        }
    }
}