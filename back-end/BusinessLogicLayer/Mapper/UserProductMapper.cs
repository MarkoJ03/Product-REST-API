using BusinessLogicLayer.DTOs;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Mapper
{
    public static class UserProductMapper
    {

/*        public static UserProduct ToEntity(UserProductDTO userProductDto)
        {
            return new UserProduct(userProductDto.UserId, userProductDto.User, userProductDto.ProductId, userProductDto.Product);
        }
*/
        public static UserProductDTO ToDto(UserProduct userProduct)
        {
            return new UserProductDTO(userProduct.UserId, userProduct.User, userProduct.ProductId, userProduct.Product);
        }
    }
}