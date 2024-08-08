using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetUserByID(int userId);
        public Task<User> InsertUser(User user);
        public Task<User> DeleteUser(int userID);
        public Task<User> UpdateUser(User user);
        public Task<User> GetUserByUsername(string username);

    }
}
