using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Mapper.BusinessLogicLayer.Mappers;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class UserProductService : IUserProductService
    {
        private readonly IUserProductRepository _userProductRepository;

        public UserProductService(IUserProductRepository userProductRepository)
        {
            _userProductRepository = userProductRepository;
        }

        public async Task AssignProductToUser(int userId, int productId)
        {
            await _userProductRepository.AssignProductToUser(userId, productId);
        }

        public async Task<bool> IsProductOwner(int userId, int productId)
        {
            return await _userProductRepository.IsProductOwner(userId, productId);
        }

        public async Task<IEnumerable<ProductDTO>> GetUserProducts(int userId)
        {
            var products = await _userProductRepository.GetUserProducts(userId);
            return products.Select(ProductMapper.ToDto);
        }
    }
}
