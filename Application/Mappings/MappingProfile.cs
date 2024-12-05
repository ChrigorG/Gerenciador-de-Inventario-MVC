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

            CreateMap<Employee, EmployeeDTO>().IncludeBase<BaseEntities, BaseDTO>();
            CreateMap<PermissionGroup, PermissionGroupDTO>().IncludeBase<BaseEntities, BaseDTO>();
            CreateMap<Product, ProductDTO>().IncludeBase<BaseEntities, BaseDTO>();
            CreateMap<StockMovements, StockMovementsDTO>().IncludeBase<BaseEntities, BaseDTO>();
        }
    }
}
