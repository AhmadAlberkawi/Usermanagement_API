using DataAccess.DbAccess;
using DataAccess.DTOs;
using DataAccess.Models;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlDataAccess _db;

        public UserRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<UserDto>> GetUsers()
        {
            return _db.LoadData<UserDto, dynamic>(storedProcedure: "dbo.spUser_GetAll", new { });
        }

        public async Task<User?> GetUserById(int id)
        {
            var results = await _db.LoadData<User, dynamic>(
                storedProcedure: "dbo.spUser_GetById", new { Id = id });

            return results.FirstOrDefault();
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            var results = await _db.LoadData<User, dynamic>(
                 storedProcedure: "dbo.spUser_GetByUsername", new { Username = username });

            return results.FirstOrDefault();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var results = await _db.LoadData<User, dynamic>(
                 storedProcedure: "dbo.spUser_GetByEmail", new { Email = email });

            return results.FirstOrDefault();
        }

        public async Task<bool> ExistsByUsername(string username)
        {
            var results = await _db.LoadData<User, dynamic>(
                 storedProcedure: "dbo.spUser_GetByUsername", new { Username = username });

            return results.Any();
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            var results = await _db.LoadData<User, dynamic>(
                  storedProcedure: "dbo.spUser_GetByEmail", new { Email = email });

            return results.Any();
        }

        public async Task<int> AddUser(User user)
        {
            return await _db.InsertData(storedProcedure: "dbo.spUser_Create", new
            {
                user.Username,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Photo,
                user.Role,
                user.PasswordHash,
                user.PasswordSalt
            });
        }

        public Task UpdateUser(User user)
        {
            return _db.SaveData(storedProcedure: "dbo.spUser_Update", new
            {
                user.Username,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Photo
            });
        }

        public Task DeleteUser(int id)
        {
            return _db.SaveData(storedProcedure: "dbo.spUser_Delete", new { Id = id });
        }
    }
}
