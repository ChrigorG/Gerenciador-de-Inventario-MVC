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
    public class ProductService : IProductService
    {
        private readonly IPermissionGroupRepository _permissionGroupRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,
            IPermissionGroupRepository permissionGroupRepository,
            IHttpContextAccessor httpContextAccessor,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _permissionGroupRepository = permissionGroupRepository;
            _httpContextAccessor = httpContextAccessor;
            _employeeRepository = employeeRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ProductDTO GetProduct()
        {
            ProductDTO? productDTO = new ProductDTO();
            string permission = this.GetPermission();
            if (permission == Constants.PermissionDenied)
            {
                productDTO.StatusErroMessage = true;
                productDTO.Message = "Acesso negado!";
                return productDTO;
            }

            productDTO.Title = "Produtos";
            productDTO.ListProducts = GetList();
            return productDTO;
        }

        public ProductDTO FormProduct(int id)
        {
            ProductDTO? productDTO = new ProductDTO();
            string permission = this.GetPermission();
            if (permission != Constants.PermissionAccess)
            {
                productDTO.StatusErroMessage = true;
                productDTO.Message = "Acesso negado!";
                return productDTO;
            }

            // Se o id for nullo ou zero será tratado como um novo produto
            if (Util.IsNullOrZero(id))
            {
                productDTO.Title = "Adicionar um Produto";
                return productDTO;
            }

            // A partir daqui seria somente para atualização do produto
            Product? products = _productRepository.Get(id);
            productDTO = _mapper.Map<ProductDTO?>(products);

            if (productDTO == null)
            {
                return NotFound(new ProductDTO());
            }

            productDTO.Title = $"Atualizar os dados do produto {productDTO.Id} - {productDTO.Name}";
            return productDTO;
        }

        public ProductDTO SaveProduct(ProductDTO productDTO)
        {
            string permission = this.GetPermission();
            if (permission != Constants.PermissionAccess)
            {
                productDTO.StatusErroMessage = true;
                productDTO.Message = "Acesso negado!";
                return productDTO;
            }

            productDTO.ValidatedDTO();
            if (productDTO.StatusErroMessage)
            {
                return productDTO;
            }

            Product? product;
            // Adicionar um novo Funcionário
            if (Util.IsNullOrZero(productDTO.Id))
            {
                product = _mapper.Map<Product>(productDTO);
                product = _productRepository.Add(product);
                
                if (product == null)
                {
                    return InternalServerError(productDTO, $"salvar os dados do funcionário {product!.Name}");
                }
            } else // Atualizar o Funcionário
            {
                product = _productRepository.Get(productDTO.Id);
                if (product == null)
                {
                    return NotFound(productDTO);
                }

                product = ConvertDtoToModel(product, productDTO);
                product = _productRepository.Update(product);
                if (product == null)
                {
                    return InternalServerError(productDTO, $"atualizar o funcionário {product!.Id} - {product!.Name}");
                }
            }

            productDTO = _mapper.Map<ProductDTO>(product);
            productDTO.ListProducts = GetList();
            return productDTO;
        }

        public ProductDTO DeleteProduct(int id)
        {
            ProductDTO? productDTO = new ProductDTO();
            string permission = this.GetPermission();
            if (permission != Constants.PermissionAccess)
            {
                productDTO.StatusErroMessage = true;
                productDTO.Message = "Acesso negado!";
                return productDTO;
            }

            Product? product = _productRepository.Get(id);

            if (product == null)
            {
                return NotFound(new ProductDTO());
            }

            product = _productRepository.Delete(product);
            if (product == null)
            {
                return InternalServerError(new ProductDTO(), $"deletar o produto {product!.Id} - {product!.Name}");
            }

            productDTO = _mapper.Map<ProductDTO>(product);
            productDTO.ListProducts = GetList();
            productDTO.Message = $"Produto {product.Id} - {product.Name} deletado com sucesso!";
            return productDTO;
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

        private List<ProductDTO> GetList()
        {
            List<Product> products = _productRepository.Get();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        private Product ConvertDtoToModel(Product product, ProductDTO productDTO)
        {
            product.Description = productDTO.Description;
            product.UnitType = productDTO.UnitType;
            product.Status = productDTO.Status;
            product.Price = productDTO.Price;
            product.Name = productDTO.Name;
           
            return product;
        }

        private ProductDTO NotFound(ProductDTO productDTO)
        {
            productDTO.StatusErroMessage = true;
            productDTO.Message = "Nenhum produto encontrado!";
            return productDTO;
        }

        private ProductDTO InternalServerError(ProductDTO productDTO, string complementMessage)
        {
            productDTO.StatusErroMessage = true;
            productDTO.Message = $"Ops, não conseguimos {complementMessage}, tente mais tarde!";
            return productDTO;
        }
    }
}