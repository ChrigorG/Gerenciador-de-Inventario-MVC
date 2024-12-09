using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Data.Repositories;
using Domain.Interfaces;
using Helper.Infra;
using Microsoft.Extensions.DependencyInjection;
using Shared.Helper.Services.Interface;

namespace InfrastructureIoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureIoC(this IServiceCollection services)
        {
            // Uso do Claim
            services.AddHttpContextAccessor();

            services.AddScoped<IHomeService, HomeService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStockMovementsRepository, StockMovementsRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPermissionGroupRepository, PermissionGroupRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStockMovementsService, StockMovementsService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPermissionGroupService, PermissionGroupService>();
            services.AddScoped<ILoginService, LoginService>();

            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<InitDbService>();

            // Configuração do AutoMapper
            // Configura o AutoMapper para registrar todos os perfis de mapeamento
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
