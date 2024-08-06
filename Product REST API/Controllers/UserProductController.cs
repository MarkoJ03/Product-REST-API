using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Product_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProductController : ControllerBase
    {
        private readonly IUserProductService _userProductService;

        public UserProductController(IUserProductService userProductService)
        {
            _userProductService = userProductService;
        }

        [HttpPost("assign")]
        //[Authorize]
        public async Task<IActionResult> AssignProductToUser([FromQuery] int userId, [FromQuery] int productId)
        {
            await _userProductService.AssignProductToUser(userId, productId);
            return NoContent();
        }

        [HttpGet("user/{userId}/products")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetUserProducts(int userId)
        {
            var products = await _userProductService.GetUserProducts(userId);
            return Ok(products);
        }

        [HttpGet("isowner")]
        //[Authorize]
        public async Task<IActionResult> IsProductOwner([FromQuery] int userId, [FromQuery] int productId)
        {
            var isOwner = await _userProductService.IsProductOwner(userId, productId);
            return Ok(new { IsOwner = isOwner });
        }
    }
}
