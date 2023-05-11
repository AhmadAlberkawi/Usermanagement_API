using DataAccess.DTOs;
using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IUserRepository
    {
        Task<int> AddUser(User user);
        Task DeleteUser(int id);
        Task<bool> ExistsByEmail(string email);
        Task<bool> ExistsByUsername(string username);
        Task<IEnumerable<UserDto>> GetUsers();
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByUsername(string username);
        Task UpdateUser(User user);
    }
}