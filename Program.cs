using backend.Models;
using backend.Models.Database.DatabaseModels;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Services
            builder.Services.AddScoped<LoginService>();
            builder.Services.AddScoped<CariService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<SistemDbContext>();
            builder.Services.AddSingleton<Test2025DbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:3000")  // Frontend adresi
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

                // E�er HTTPS kullan�yorsan bunu kullan (�rn: https://localhost:7030)
                options.Cookie.SameSite = SameSiteMode.None;      // Cross-site cookie i�in �nemli
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  // HTTPS zorunluysa

                // E�er HTTPS kullanm�yorsan bunu kullan:
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

            app.UseCors();       // CORS middleware burada olmal�
            app.UseSession();    // Session middleware CORS'tan sonra, Auth'dan �nce
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
