using Microsoft.EntityFrameworkCore;
using NetCore_Angular_Demo.Models;

namespace NetCore_Angular_Demo.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(vf =>
            new
            {
                vf.VehicleId,
                vf.FeatureId
            });
        }
    }
}
