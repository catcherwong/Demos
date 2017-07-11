using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AuthorizedServer
{
    public class DemoDbContext : DbContext
    {
        public DbSet<RToken> RTokens { get; set; }

        //public DbSet<AToken> ATokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connStr = Path.Combine(Directory.GetCurrentDirectory(), "demo.db");

            optionsBuilder.UseSqlite($"Data Source={connStr}");
        }
    }
}
