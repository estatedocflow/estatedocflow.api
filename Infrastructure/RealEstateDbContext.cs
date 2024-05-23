using Microsoft.EntityFrameworkCore;

namespace estatedocflow.api.Infrastructure
{
    public class RealEstateDbContext : DbContext
    {
        private IConfiguration _config;
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options, IConfiguration config) : base(options)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config.GetConnectionString("DefaultConnection"), opt =>
            {
                opt.MigrationsHistoryTable("_GlobleMigrationHistory", "base");
            });
        }

        //Tables

        //public DbSet<House> Houses { get; set; }
        //public DbSet<HousePhoto> HousePhotos { get; set; }
    }
}
