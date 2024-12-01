using Application.Interfaces;
using Application.Services;
using Data.Repositories;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureIoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureIoC(this IServiceCollection services)
        {
            services.AddScoped<IHomeService, HomeService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStockMovementsRepository, StockMovementsRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPermissionGroupRepository, PermissionGroupRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStockMovementsService, StockMovementsService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPermissionGroupService, PermissionGroupService>();

            return services;
        }
    }
}
