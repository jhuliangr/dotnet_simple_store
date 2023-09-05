using Store.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Store.Server.Data
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Thing> Things => Set<Things>();

        public StoreDbContext(DbContextOptions<StoreDbContext> options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace with your connection string.
            var connectionString = $"server=localhost;user={};password={};database=store";

            var serverVersion = new MySqlServerVersion(new Version(10, 4, 11));
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
    }
}