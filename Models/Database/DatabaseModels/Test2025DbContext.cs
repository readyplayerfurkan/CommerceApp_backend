using System.Data;
using Microsoft.Data.SqlClient;

namespace backend.Models.Database.DatabaseModels;

public class Test2025DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public Test2025DbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("GWTest2025Connection");
    }

    public IDbConnection GetConnection() => new SqlConnection(_connectionString);
}