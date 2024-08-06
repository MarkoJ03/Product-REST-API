using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(int id);
        Task<UserDTO> AddUser(UserDTO userDto);
        Task<UserDTO> UpdateUser(UserDTO userDto);
        Task DeleteUser(int id);

        Task<UserDTO> GetUserByUsername(string username);
    }

}

