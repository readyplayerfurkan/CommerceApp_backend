using backend.Models;
using backend.Models.Database.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.CookiePolicy;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Services
            builder.Services.AddScoped<LoginService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GWSistemDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("GWSistemConnection")));

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:3000")  // Frontend adresin
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();   // Cookie gönderimi için zorunlu
                });
            });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

                // Eðer HTTPS kullanýyorsan bunu kullan (Örn: https://localhost:7030)
                options.Cookie.SameSite = SameSiteMode.None;      // Cross-site cookie için önemli
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  // HTTPS zorunluysa

                // Eðer HTTPS kullanmýyorsan bunu kullan:
                // options.Cookie.SecurePolicy = CookieSecurePolicy.None;
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();       // CORS middleware burada olmalý
            app.UseSession();    // Session middleware CORS'tan sonra, Auth'dan önce
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
