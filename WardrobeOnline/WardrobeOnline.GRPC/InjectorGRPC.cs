using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

using WardrobeOnline.GRPC.Services.Implementations;

namespace WardrobeOnline.GRPC
{
    public static class InjectorGRPC
    {
        private static bool injected = false;
        public static IServiceCollection InjectGRPC(this IServiceCollection services)
        {
            services.AddGrpc(options => {
                options.EnableDetailedErrors = true;
            });

            injected = true;
            return services;
        }

        public static IEndpointRouteBuilder MapGRPC(this IEndpointRouteBuilder app)
        {
            if (injected) {
                app.MapGrpcService<GRPCDatabaseUploader>();

                app.MapGet("/", () => {
                    Console.WriteLine("Запрос по корню");
                });
            }

            return app;
        }
    }
}
