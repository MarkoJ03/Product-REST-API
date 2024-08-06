using DataAccessLayer.Data;
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> DeleteUser(int userID)
        {

            var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == userID);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user;

        }

        public async Task<User> GetUserByID(int userId)
        {

            var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == userId);
            return user;

        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> InsertUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;

        }

        public async Task<User> UpdateUser(User updatedUser)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(e => e.Id == updatedUser.Id);
            user!.Username = updatedUser.Username;
            user.Password = updatedUser.Password;
            user.Role = updatedUser.Role;
            await _context.SaveChangesAsync();
            return user;


        }
    }
}
    

