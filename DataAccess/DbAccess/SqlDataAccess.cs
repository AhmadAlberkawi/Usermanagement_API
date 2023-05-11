using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace DataAccess.DbAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "Usermanagement_DB")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> InsertData<T>(string storedProcedure, T parameters, string connectionId = "Usermanagement_DB")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            return await connection.ExecuteScalarAsync<int>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> SaveData<T>(string storedProcedure, T parameters, string connectionId = "Usermanagement_DB")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            return await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        //public bool IsExist<T>(string storedProcedure, T parameters, string connectionId = "Messaging_DB")
        //{
        //    using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        //    return connection.Query<bool>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //}

    }
}
