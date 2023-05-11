namespace DataAccess.DbAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "Usermanagement_DB");
        Task<int> InsertData<T>(string storedProcedure, T parameters, string connectionId = "Usermanagement_DB");
        Task<int> SaveData<T>(string storedProcedure, T parameters, string connectionId = "Usermanagement_DB");
    }
}