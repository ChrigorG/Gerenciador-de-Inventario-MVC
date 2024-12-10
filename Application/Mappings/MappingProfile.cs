using Application.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento entre BaseEntities e BaseDTO
            CreateMap<BaseEntities, BaseDTO>();

            CreateMap<EmployeeDTO, Employee>();
            CreateMap<Employee, EmployeeDTO>().IncludeBase<BaseEntities, BaseDTO>();

            CreateMap<PermissionGroupDTO, PermissionGroup>();
            CreateMap<PermissionGroup, PermissionGroupDTO>().IncludeBase<BaseEntities, BaseDTO>();

            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>().IncludeBase<BaseEntities, BaseDTO>();

            CreateMap<StockMovementsDTO, StockMovements>();
            CreateMap<StockMovements, StockMovementsDTO>().IncludeBase<BaseEntities, BaseDTO>();
        }
    }
}
