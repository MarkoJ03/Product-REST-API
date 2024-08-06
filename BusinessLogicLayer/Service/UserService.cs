using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Mapper;
using BusinessLogicLayer.Mapper.BusinessLogicLayer.Mappers;
using DataAccessLayer.Migrations;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetUsers();
            return users.Select(UserMapper.ToDto);
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByID(id);
            return user != null ? UserMapper.ToDto(user) : null;
        }

        public async Task<UserDTO> AddUser(UserDTO userDto)
        {

            var user = UserMapper.ToEntity(userDto);
            var newUser = await _userRepository.InsertUser(user);
            return UserMapper.ToDto(newUser);
        }

        public async Task<UserDTO> UpdateUser(UserDTO userDto)
        {
            var user = UserMapper.ToEntity(userDto);
            var updatedUser = await _userRepository.UpdateUser(user);
            return UserMapper.ToDto(updatedUser);
        }


        public async Task<UserDTO> GetUserByUsername(string username)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null)
            {
                return null;
            }

            return new UserDTO(user.Id,user.Username,user.Password,user.Role);
          
        }
        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }
    }
}
