
using AssetManagement.Repositories.IRepos.IDbChecker;
using Microsoft.Data.SqlClient;

namespace AssetManagement.Repositories.Repos.DbChecker
{
    public class DbChecker : IDbChecker
    {
        public async Task<bool> IsDbConnectedAsync(string connectionString)
        {
            try
            {
                await using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection failed: {ex.Message}");
                return false;
            }
        }
    }
}