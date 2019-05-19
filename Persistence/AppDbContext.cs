using Microsoft.EntityFrameworkCore;
using NetCore_Angular_Demo.Models;

namespace NetCore_Angular_Demo.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
    }
}
