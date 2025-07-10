using Microsoft.EntityFrameworkCore;
using RetailxAPI.Data.Entities;
using RetailxAPI.Data.Models;

namespace RetailxAPI.Data.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> GetUsers()
        {
            return await _context.Users
                .Select(user => new UserModel
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Passwd = user.Passwd,
                    Statu = user.Statu,
                    IsActive = user.IsActive
                })
                .ToListAsync();
        }
        public async Task<UserModel?> GetUserById(short userId)
        {
            return await _context.Users
                .Where(user => user.UserId == userId)
                .Select(user => new UserModel
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Passwd = user.Passwd,
                    Statu = user.Statu,
                    IsActive = user.IsActive
                })
                .FirstOrDefaultAsync();
        }
        public async Task<UserModel?> GetUserByUserName(string userName)
        {
            return await _context.Users
                .Where(user => user.UserName == userName)
                .Select(user => new UserModel
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Passwd = user.Passwd,
                    Statu = user.Statu,
                    IsActive = user.IsActive
                })
                .FirstOrDefaultAsync();
        }
        public async Task<bool> AddUser(UserModel userModel)
        {
            var user = new User
            {
                UserName = userModel.UserName,
                Passwd = userModel.Passwd,
                Statu = userModel.Statu,
                IsActive = userModel.IsActive
            };

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> UpdateUser(UserModel userModel)
        {
            var user = await _context.Users.FindAsync(userModel.UserId);
            if (user == null)
            {
                return false;
            }
            user.UserName = userModel.UserName;
            user.Passwd = userModel.Passwd;
            user.Statu = userModel.Statu;
            user.IsActive = userModel.IsActive;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteUser(short userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> AdminUserLogin(string userName)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(user => user.UserName == userName);
            if (existingUser == null)
            {
                return false;
            }
            if (existingUser.Statu == "Admin" && existingUser.IsActive == 1)
            {
                return true;
            }
            return false;
        }
    }
}
