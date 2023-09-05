using Store.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Store.Server.Data
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Thing> Things => Set<Thing>();

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }
    }
}