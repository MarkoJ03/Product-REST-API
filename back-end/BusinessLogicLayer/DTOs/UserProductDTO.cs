using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class UserProductDTO
    {
        public UserProductDTO(int userId, User user, int productId, Product product)
        {
            UserId = userId;
            User = user;
            ProductId = productId;
            Product = product;
        }

        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
