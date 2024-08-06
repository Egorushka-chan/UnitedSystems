using Microsoft.Extensions.DependencyInjection;

namespace WardrobeOnline.GRPC
{
    public static class InjectorGRPC
    {
        public static IServiceCollection InjectGRPC(this IServiceCollection services)
        {
            return services;
        }
    }
}
