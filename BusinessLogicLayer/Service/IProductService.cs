using BusinessLogicLayer.DTOs;
using DataAccessLayer.Model;

namespace BusinessLogicLayer.Service
{
    public interface Interface
    {
        public interface IProductService
        {
            Task<IEnumerable<ProductDTO>> GetAllProducts();
            Task<ProductDTO> GetProductById(int id);
            Task<ProductDTO> AddProduct(ProductDTO productDto);
            Task<ProductDTO> UpdateProduct(ProductDTO productDto);
            Task DeleteProduct(int id);

            Task<ProductStatisticsDTO> GetProductStatistics();

            Task<IEnumerable<PopularProductDTO>> GetMostPopularProducts(int topCount);
        }

    }
}
