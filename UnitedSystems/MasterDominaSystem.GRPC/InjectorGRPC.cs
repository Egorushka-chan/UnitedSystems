using MasterDominaSystem.GRPC.Models;
using MasterDominaSystem.GRPC.Services.Implementations;
using MasterDominaSystem.GRPC.Services.Interfaces;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MasterDominaSystem.GRPC
{
    public static class InjectorGRPC
    {
        private const string OptionsPath = "gRPC";
        public static IHostApplicationBuilder InjectGRPC(this IHostApplicationBuilder builder)
        {
            builder.Services.Configure<ConnectionGRPCSettings>(builder.Configuration.GetSection(OptionsPath));
            builder.Services.AddTransient<IDatabaseDownloader, GRPCDatabaseDownloader>();

            return builder;
        }
    }
}
