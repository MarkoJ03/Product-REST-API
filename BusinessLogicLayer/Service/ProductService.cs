using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Mapper;
using BusinessLogicLayer.Mapper.BusinessLogicLayer.Mappers;
using DataAccessLayer.Model;
using DataAccessLayer.Repository;
using static BusinessLogicLayer.Service.Interface;

namespace BusinessLogicLayer.Service
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IUserProductRepository _userProductRepository;

        public ProductService(IProductRepository productRepository, IUserProductRepository userProductRepository)
        {
            _productRepository = productRepository;
            _userProductRepository = userProductRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var products = await _productRepository.GetProducts();
            return products.Select(ProductMapper.ToDto);
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByID(id);
            return product != null ? ProductMapper.ToDto(product) : null;
        }

        public async Task<ProductDTO> AddProduct(ProductDTO productDto) { 
        
            var product = ProductMapper.ToEntity(productDto);
            var newProduct = await _productRepository.InsertProduct(product);
            return ProductMapper.ToDto(newProduct);
        }

        public async Task<ProductDTO> UpdateProduct(ProductDTO productDto)
        {
            var product = ProductMapper.ToEntity(productDto);
            var updatedProduct = await _productRepository.UpdateProduct(product);
            return ProductMapper.ToDto(updatedProduct);
        }

        public async Task DeleteProduct(int id)
        {
            await _productRepository.DeleteProduct(id);
        }


        public async Task<ProductStatisticsDTO> GetProductStatistics()
        {
            var products = await _productRepository.GetProducts();
            var totalProducts = products.Count();
            var averagePrice = products.Average(p => p.Price);
            var minPrice = products.Min(p => p.Price);
            var maxPrice = products.Max(p => p.Price);
            var totalAssignments = await _userProductRepository.GetTotalAssignments();

            return new ProductStatisticsDTO
            {
                TotalProducts = totalProducts,
                AveragePrice = averagePrice,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                TotalAssignments = totalAssignments
            };
        }
    }
}
