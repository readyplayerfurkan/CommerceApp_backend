using backend.Models.Database.DatabaseModels;
using backend.Models.Database.TableModels;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace backend.Models
{
    public class LoginService
    {
        private readonly GWSistemDbContext _gwSistemDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(GWSistemDbContext gwSistemDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _gwSistemDbContext = gwSistemDbContext;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> LoginUser(string username, string password)
        {
            var user = await _gwSistemDbContext.Users.FirstOrDefaultAsync(u => u.KOD == username && u.SIFRE == password);
          
            if (user != null)
                return user;

            return null;
        }
    }
}
