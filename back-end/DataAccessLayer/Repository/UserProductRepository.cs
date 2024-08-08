using DataAccessLayer.Data;
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class UserProductRepository : IUserProductRepository
    {
        private readonly AppDbContext _context;

        public UserProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AssignProductToUser(int userId, int productId)
        {
            var userProduct = new UserProduct { UserId = userId, ProductId = productId };
            _context.UserProducts.Add(userProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsProductOwner(int userId, int productId)
        {
            return await _context.UserProducts
                                 .AnyAsync(up => up.UserId == userId && up.ProductId == productId);
        }

        public async Task<IEnumerable<Product>> GetUserProducts(int userId)
        {
            return await _context.UserProducts
                                 .Where(up => up.UserId == userId)
                                 .Select(up => up.Product)
                                 .ToListAsync();
        }

        public async Task<int> GetTotalAssignments()
        {
            return await _context.UserProducts.CountAsync();
        }

        public async Task<IEnumerable<PopularProduct>> GetPopularProducts()
        {
            return await _context.UserProducts
                .GroupBy(up => up.Product)
                .Select(g => new PopularProduct
                {
                    ProductId = g.Key.Id,
                    ProductName = g.Key.Name,
                    AssignmentsCount = g.Count()
                })
                .Where(p => p.AssignmentsCount > 0)
                .OrderByDescending(p => p.AssignmentsCount)
                .ToListAsync();
        }
    }
}
