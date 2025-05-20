using backend.Models.Database.DatabaseModels;
using backend.Models.Database.TableModels;
using Dapper;

namespace backend.Models
{
    public class LoginService
    {
        private readonly SistemDbContext _sistemDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IHttpContextAccessor httpContextAccessor, SistemDbContext sistemDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _sistemDbContext = sistemDbContext;
        }

        public async Task<User> LoginUserAsync(string username, string password)
        {
            var query = "SELECT TOP 1 * FROM KULLANICI WHERE KOD = @UserName AND SIFRE = @Password";
            
            using var connection = _sistemDbContext.CreateConnection();
            var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { UserName = username, Password = password });
            
            return user ?? null;
        }
    }
}
