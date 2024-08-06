
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Data;
using DataAccessLayer.Model;
using System;

namespace DataAccessLayer.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> DeleteProduct(int productID)
        {
        
            var product = await _context.Products.FirstOrDefaultAsync(e => e.Id == productID);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return product;
          
        }

        public async Task<Product> GetProductByID(int productId)
        {
        
            var product = await _context.Products.FirstOrDefaultAsync(e => e.Id == productId);
            return product;
            
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> InsertProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task<Product> UpdateProduct(Product updatedProduct) { 
                Product? product = await _context.Products.FirstOrDefaultAsync(e => e.Id == updatedProduct.Id);
                product!.Name = updatedProduct.Name;
                product.Description = updatedProduct.Description;
                product.Price = updatedProduct.Price;
                await _context.SaveChangesAsync();
                return product;
            
        
    }
    }
}
