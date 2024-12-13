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
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public StockMovementsService(IStockMovementsRepository stockMovementsRepository,
            IPermissionGroupRepository permissionGroupRepository,
            IHttpContextAccessor httpContextAccessor,
            IEmployeeRepository employeeRepository,
            IProductRepository productRepository,
            IProductService productService,
            IMapper mapper)
        {
            _permissionGroupRepository = permissionGroupRepository;
            _stockMovementsRepository = stockMovementsRepository;
            _httpContextAccessor = httpContextAccessor;
            _employeeRepository = employeeRepository;
            _productRepository = productRepository;
            _productService = productService;
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

        public StockMovementsDTO FormProductInStock()
        {
            StockMovementsDTO? stockMovementsDTO = new StockMovementsDTO();
            string permission = this.GetPermission();
            if (permission != Constants.PermissionAccess)
            {
                stockMovementsDTO.StatusErroMessage = true;
                stockMovementsDTO.Message = "Acesso negado!";
                return stockMovementsDTO;
            }

            stockMovementsDTO.ListProduct = _productService.GetList();
            stockMovementsDTO.Title = "Dar entrada no estoque";
            return stockMovementsDTO;
        }

        public StockMovementsDTO FormRemoveProductInStock()
        {
            StockMovementsDTO? stockMovementsDTO = new StockMovementsDTO();
            string permission = this.GetPermission();
            if (permission != Constants.PermissionAccess)
            {
                stockMovementsDTO.StatusErroMessage = true;
                stockMovementsDTO.Message = "Acesso negado!";
                return stockMovementsDTO;
            }

            stockMovementsDTO.ListProduct = _productService.GetList();
            stockMovementsDTO.Title = "Venda / Saida de Produto";
            return stockMovementsDTO;
        }

        public StockMovementsDTO AddProductInStock(StockMovementsDTO stockMovementsDTO)
        {
            string permission = this.GetPermission();
            if (permission != Constants.PermissionAccess)
            {
                stockMovementsDTO.StatusErroMessage = true;
                stockMovementsDTO.Message = "Acesso negado!";
                return stockMovementsDTO;
            }

            stockMovementsDTO.ValidatedDTO();
            if (stockMovementsDTO.StatusErroMessage)
            {
                return stockMovementsDTO;
            }

            StockMovements? stockMovements = _mapper.Map<StockMovements>(stockMovementsDTO);
            stockMovements.MovementDate = DateTime.Now;
            stockMovements.MovementType = Constants.Input;
            stockMovements = _stockMovementsRepository.Add(stockMovements);
            if (stockMovements == null)
            {
                return InternalServerError(stockMovementsDTO, $"adicionar o produto no Estoque");
            }

            stockMovementsDTO = _mapper.Map<StockMovementsDTO>(stockMovements);
            stockMovementsDTO.Message = "Produto adicionado no estoque com sucesso!";
            return stockMovementsDTO;
        }

        public StockMovementsDTO RemoveProductInStock(StockMovementsDTO stockMovementsDTO)
        {
            string permission = this.GetPermission();
            if (permission != Constants.PermissionAccess)
            {
                stockMovementsDTO.StatusErroMessage = true;
                stockMovementsDTO.Message = "Acesso negado!";
                return stockMovementsDTO;
            }

            stockMovementsDTO.ValidatedDTO();
            if (stockMovementsDTO.StatusErroMessage)
            {
                return stockMovementsDTO;
            }

            int quantityInStock = _stockMovementsRepository.SumQuantityProductInStock(stockMovementsDTO.IdProduct);
            if (!this.CanRemoveQuantityProduct(quantityInStock, stockMovementsDTO.Quantity))
            {
                stockMovementsDTO.StatusErroMessage = true;
                stockMovementsDTO.Message = $"Você não pode retirar essa quantidade de produto, a quantidade no estoque é {quantityInStock}.";
                return stockMovementsDTO;
            }

            StockMovements? stockMovements = _mapper.Map<StockMovements>(stockMovementsDTO);
            stockMovements.MovementDate = DateTime.Now;
            stockMovements.MovementType = Constants.Output;
            stockMovements = _stockMovementsRepository.Add(stockMovements);
            if (stockMovements == null)
            {
                return InternalServerError(stockMovementsDTO, $"remover o produto do Estoque");
            }

            stockMovementsDTO = _mapper.Map<StockMovementsDTO>(stockMovements);
            stockMovementsDTO.Message = "Produto removido do estoque com sucesso!";
            return stockMovementsDTO;
        }

        public StockMovementsDTO DetailProductInStock(int id)
        {
            StockMovementsDTO? stockMovementsDTO = new StockMovementsDTO();
            string permission = this.GetPermission();
            if (permission == Constants.PermissionDenied)
            {
                stockMovementsDTO.StatusErroMessage = true;
                stockMovementsDTO.Message = "Acesso negado!";
                return stockMovementsDTO;
            }
            
            StockMovements? stockMovements = _stockMovementsRepository.Get(id);
            if (stockMovements == null)
            {
                return NotFound(stockMovementsDTO);
            }

            stockMovementsDTO = _mapper.Map<StockMovementsDTO>(stockMovements);
            Product? product = _productRepository.Get(stockMovementsDTO.IdProduct);
            Employee? employee = _employeeRepository.Get(stockMovementsDTO.IdUserCreated);

            stockMovementsDTO.NameUserCreated = employee?.Name ?? "(Funcionario que criou foi desviculado do sistema)";
            stockMovementsDTO.ProductName = product?.Name ?? "Produto não está mais disponível";
            stockMovementsDTO.Title = "Detalhe da Movimentação";
            return stockMovementsDTO;
        }

        private bool CanRemoveQuantityProduct(int quantityInStock, int quantityToRemove)
        {
            return quantityInStock > 0 && quantityInStock <= quantityToRemove ? true : false;
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

        private StockMovementsDTO NotFound(StockMovementsDTO stockMovementsDTO)
        {
            stockMovementsDTO.StatusErroMessage = true;
            stockMovementsDTO.Message = "Elemento do estoque não encontrado!";
            return stockMovementsDTO;
        }

        private StockMovementsDTO InternalServerError(StockMovementsDTO stockMovementsDTO, string complementMessage)
        {
            stockMovementsDTO.StatusErroMessage = true;
            stockMovementsDTO.Message = $"Ops, não conseguimos {complementMessage}, tente mais tarde!";
            return stockMovementsDTO;
        }

        private List<StockMovementsDTO> GetList()
        {
            List<StockMovements> stockMovements = _stockMovementsRepository.Get();
            List<StockMovementsDTO> listStock = _mapper.Map<List<StockMovementsDTO>>(stockMovements);

            List<ProductDTO> listProduct = _productService.GetList();

            listStock = (from stock in listStock
                              join product in listProduct on stock.IdProduct equals product.Id
                              select new StockMovementsDTO
                              {
                                  Id = stock.Id,
                                  IdProduct = product.Id,
                                  ProductName = product.Name,
                                  UnitType = product.UnitType,
                                  MovementDate = stock.MovementDate,
                                  Quantity = stock.Quantity,
                                  MovementType = stock.MovementType,
                                  Description = stock.Description,
                              }).ToList();

            return listStock;
        }
    }
}
