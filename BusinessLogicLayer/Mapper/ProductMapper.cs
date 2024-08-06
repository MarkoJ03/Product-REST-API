using BusinessLogicLayer.DTOs;
using DataAccessLayer.Model;

namespace BusinessLogicLayer.Mapper
{
    namespace BusinessLogicLayer.Mappers
    {
        public static class ProductMapper
        {

                public static Product ToEntity(ProductDTO productDto)
                {
                    return new Product(productDto.Id, productDto.Name, productDto.Price, productDto.Description);
                }

                public static ProductDTO ToDto(Product product)
                {
                    return new ProductDTO(product.Id, product.Name, product.Price, product.Description);
                }
            }
        }
    }
    
