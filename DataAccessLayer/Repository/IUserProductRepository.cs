using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{


    public interface IUserProductRepository
    {
        Task AssignProductToUser(int userId, int productId);
        Task<bool> IsProductOwner(int userId, int productId);
        Task<IEnumerable<Product>> GetUserProducts(int userId);

        Task<int> GetTotalAssignments();
    }
}

