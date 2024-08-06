using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Mapper;
using BusinessLogicLayer.Mapper.BusinessLogicLayer.Mappers;
using BusinessLogicLayer.Service;
using DataAccessLayer.Model;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using static BusinessLogicLayer.Service.Interface;

namespace User_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IValidator<User> _validator;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IValidator<User> validator)
        {
            _validator = validator;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new ErrorModel(500, ex.Message, ex.StackTrace?.ToString()));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorModel(500, ex.Message, ex.StackTrace?.ToString()));
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] UserDTO newUserDto)
        {
            try
            {
                ValidationResult result = await _validator.ValidateAsync(UserMapper.ToEntity(newUserDto));
                if (result.IsValid)
                {
                    var user = await _userService.AddUser(newUserDto);
                    return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
                }

                return BadRequest("Invalid user data");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorModel(500, ex.Message, ex.StackTrace?.ToString()));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO updatedUserDto)
        {
            try
            {
                var existingUser = await _userService.GetUserById(id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                ValidationResult result = await _validator.ValidateAsync(UserMapper.ToEntity(updatedUserDto));
                if (result.IsValid)
                {
                    updatedUserDto.Id = id;
                    var updatedUser = await _userService.UpdateUser(updatedUserDto);
                    return Ok(updatedUser);
                }
                return BadRequest("Invalid user data");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorModel(500, ex.Message, ex.StackTrace?.ToString()));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var existingUser = await _userService.GetUserById(id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                await _userService.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorModel(500, ex.Message, ex.StackTrace?.ToString()));
            }
        }
    }
}
