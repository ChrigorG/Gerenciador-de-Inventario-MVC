using Application.DTO;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> GetProduct();

        Task<ProductDTO> FormProduct(int id);

        Task<ProductDTO> SaveProduct(ProductDTO productDTO);

        Task<ProductDTO> DeleteProduct(int id);
    }
}
