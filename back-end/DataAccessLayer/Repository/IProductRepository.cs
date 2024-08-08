using DataAccessLayer.Model;

namespace DataAccessLayer.Repository
{
    public interface IProductRepository  
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProductByID(int productId);
        public Task<Product> InsertProduct(Product product);
        public Task<Product> DeleteProduct(int productID);
        public Task<Product> UpdateProduct(Product product);
    }
}
