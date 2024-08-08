using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public interface IUserProductService
    {
        Task AssignProductToUser(int userId, int productId);
        Task<bool> IsProductOwner(int userId, int productId);
        Task<IEnumerable<ProductDTO>> GetUserProducts(int userId);
    }
}
