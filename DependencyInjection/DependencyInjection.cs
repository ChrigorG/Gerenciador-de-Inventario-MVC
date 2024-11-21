using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureIoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureIoC(this IServiceCollection services)
        {
            services.AddScoped<IHomeService, HomeService>();

            return services;
        }
    }
}
