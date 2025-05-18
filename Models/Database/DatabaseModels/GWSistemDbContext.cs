using backend.Models.Database.TableModels;
using Microsoft.EntityFrameworkCore;

namespace backend.Models.Database.DatabaseModels
{
    public class GWSistemDbContext : DbContext
    {
        public GWSistemDbContext(DbContextOptions<GWSistemDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserAuthorization> UserAuthorizations { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("KULLANICI", schema: "dbo");
            modelBuilder.Entity<UserAuthorization>().ToTable("KULLANICI_YETKI", schema: "dbo");
            modelBuilder.Entity<Company>().ToTable("FIRMA", schema: "dbo");
        }
    }
}
