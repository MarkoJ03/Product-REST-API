using BusinessLogicLayer.DTOs;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Mapper
{
    public static class UserMapper
    {

        public static User ToEntity(UserDTO userDto)
        {
            return new User(userDto.Id, userDto.Username, userDto.Password, userDto.Role);
        }

        public static UserDTO ToDto(User user)
        {
            return new UserDTO(user.Id, user.Username, user.Password, user.Role);
        }
    }
}
    
