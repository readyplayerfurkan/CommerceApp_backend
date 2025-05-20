using System.Data;
using Microsoft.Data.SqlClient;

namespace backend.Models.Database.DatabaseModels
{
    public class SistemDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SistemDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("GWSistemConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
