using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Helper;
using System.Security.Claims;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ProductDTO GetProduct()
        {
            return new ProductDTO()
            {
                Title = "Produtos",
                ListProducts = GetList()
            };
        }

        public ProductDTO FormProduct(int id)
        {
            // Se o id for nullo ou zero será tratado como um novo produto
            if (Util.IsNullOrZero(id))
            {
                ProductDTO productDTO = new();
                productDTO.Title = "Adicionar um Produto";
                return productDTO;

            }

            // A partir daqui seria somente para atualização do produto
            Product? products = _productRepository.Get(id);
            ProductDTO? productsDTO = _mapper.Map<ProductDTO?>(products);

            if (productsDTO == null)
            {
                return NotFound(new ProductDTO());
            }

            productsDTO.Title = $"Atualizar os dados do produto {productsDTO.Id} - {productsDTO.Name}";
            return productsDTO;
        }

        public ProductDTO SaveProduct(ProductDTO productDTO)
        {
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

            ProductDTO productDTO = _mapper.Map<ProductDTO>(product);
            productDTO.ListProducts = GetList();
            productDTO.Message = $"Produto {product.Id} - {product.Name} deletado com sucesso!";
            return productDTO;
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