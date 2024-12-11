using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Shared.Helper;
using System.Security.Claims;

namespace Application.Services
{
    public class StockMovementsService : IStockMovementsService
    {
        private readonly IPermissionGroupRepository _permissionGroupRepository;
        private readonly IStockMovementsRepository _stockMovementsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public StockMovementsService(IStockMovementsRepository stockMovementsRepository,
            IPermissionGroupRepository permissionGroupRepository,
            IHttpContextAccessor httpContextAccessor,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _permissionGroupRepository = permissionGroupRepository;
            _stockMovementsRepository = stockMovementsRepository;
            _httpContextAccessor = httpContextAccessor;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public StockMovementsDTO GetStockMovements()
        {
            StockMovementsDTO? stockMovementsDTO = new StockMovementsDTO();
            string permission = this.GetPermission();
            if (permission == Constants.PermissionDenied)
            {
                stockMovementsDTO.StatusErroMessage = true;
                stockMovementsDTO.Message = "Acesso negado!";
                return stockMovementsDTO;
            }

            stockMovementsDTO.Title = "Movimentação de Estoque";
            stockMovementsDTO.ListStockMovements = GetList();
            return stockMovementsDTO;
        }

        private string GetPermission()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = 0;
            if (userIdClaim != null && int.TryParse(userIdClaim!.Value, out userId))
            {
                Employee? employee = _employeeRepository.Get(userId);
                PermissionGroup? permissionGroup = _permissionGroupRepository.Get(employee?.IdPermissionGroup ?? 0);

                return permissionGroup?.ActionPermissionGroup ?? Constants.PermissionDenied;
            }
            return Constants.PermissionDenied;
        }

        private List<StockMovementsDTO> GetList()
        {
            List<StockMovements> stockMovements = _stockMovementsRepository.Get();
            return _mapper.Map<List<StockMovementsDTO>>(stockMovements);
        }
    }
}
