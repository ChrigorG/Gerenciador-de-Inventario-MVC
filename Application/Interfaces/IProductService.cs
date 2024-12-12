using Application.DTO;

namespace Application.Interfaces
{
    public interface IProductService
    {
        ProductDTO GetProduct();
                  
        ProductDTO FormProduct(int id);
                  
        ProductDTO SaveProduct(ProductDTO productDTO);
                  
        ProductDTO DeleteProduct(int id);

        List<ProductDTO> GetList();
    }
}
