using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Helper;

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
                return NotFound();
            }

            productsDTO.Title = $"Atualizar os dados do produto {productsDTO.Id} - {productsDTO.Name}";
            return productsDTO;
        }

        public ProductDTO SaveProduct(ProductDTO productDTO)
        {
            Product? product = _mapper.Map<Product>(productDTO);
            var validationResults = ValidationEntities.Validate(product);

            if (validationResults.Count > 0)
            {
                foreach (var error in validationResults)
                {
                    productDTO.Message += $"{error.ErrorMessage}\n";
                }

                productDTO.StatusErroMessage = false;
                return productDTO;
            }

            // Adicionar um novo Produto
            if (Util.IsNullOrZero(product.Id))
            {
                product = _productRepository.Add(product);
                if (product == null)
                {
                    return InternalServerError($"salvar os dados do produto {product!.Name}");
                }
            } else // Atualizar o produto
            {
                product = _productRepository.Update(product);
                if (product == null)
                {
                    return InternalServerError($"atualizar o produto {product!.Id} - {product!.Name}");
                }
            }

            productDTO = _mapper.Map<ProductDTO>(product);
            productDTO.ListProducts = GetList();
            return productDTO;
        }

        public ProductDTO DeleteProduct(int id)
        {
            Product? product = _productRepository.Get(id);

            if (product == null){
                return NotFound();
            }

            product = _productRepository.Delete(product);
            if (product == null)
            {
                return InternalServerError($"deletar o produto {product!.Id} - {product!.Name}");
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

        private ProductDTO NotFound()
        {
            return new ProductDTO()
            {
                StatusErroMessage = true,
                Message = "Nenhum produto encontrado!"
            };
        }

        private ProductDTO InternalServerError(string complementMessage)
        {
            return new ProductDTO()
            {
                StatusErroMessage = true,
                Message = $"Ops, não conseguimos {complementMessage}, tente mais tarde!"
            };
        }
    }
}