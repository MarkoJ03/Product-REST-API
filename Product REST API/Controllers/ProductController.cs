using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static BusinessLogicLayer.Service.Interface;

namespace Product_REST_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUserProductService _userProductService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ProductController> _logger;
        private readonly IConfiguration _configuration;


        public ProductController(IProductService productService, IUserProductService userProductService, IHttpContextAccessor httpContextAccessor, ILogger<ProductController> logger, IConfiguration configuration)
        {
            _productService = productService;
            _userProductService = userProductService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> AddProduct([FromBody] ProductDTO productDto)
        {
            try
            {
                var userId = TokenHelper.GetUserIdFromToken(_httpContextAccessor.HttpContext, _logger);
                var newProduct = await _productService.AddProduct(productDto);

                await _userProductService.AssignProductToUser(userId, newProduct.Id);

                return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO productDto)
        {
            try
            {
                var userId = TokenHelper.GetUserIdFromToken(_httpContextAccessor.HttpContext, _logger);
                var isOwner = await _userProductService.IsProductOwner(userId, id);
                if (!isOwner)
                {
                    return Forbid("You are not the owner of this product.");
                }

                var updatedProduct = await _productService.UpdateProduct(productDto);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var userId = TokenHelper.GetUserIdFromToken(_httpContextAccessor.HttpContext, _logger);
                var isOwner = await _userProductService.IsProductOwner(userId, id);
                if (!isOwner)
                {
                    return Forbid("You are not the owner of this product.");
                }

                await _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<ProductStatisticsDTO>> GetProductStatistics()
        {
            try
            {
                var statistics = await _productService.GetProductStatistics();
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<PopularProductDTO>>> GetMostPopularProducts([FromQuery] int? topCount)
        {
            try
            {
                int defaultTopCount = _configuration.GetValue<int>("DefaultTopPopularProducts", 10);
                int count = topCount ?? defaultTopCount;
                var popularProducts = await _productService.GetMostPopularProducts(count);
                return Ok(popularProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
