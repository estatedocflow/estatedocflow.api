using Microsoft.EntityFrameworkCore;
using estatedocflow.api.Models.Entities;

namespace estatedocflow.api.Infrastructure
{
    public class RealEstateDbContext(
        DbContextOptions<RealEstateDbContext> options,
        IConfiguration config,
        DbSet<House> houses,
        DbSet<HousePhoto> housePhotos)
        : DbContext(options)
    {
        private readonly IConfiguration _config = config ?? throw new ArgumentNullException(nameof(config));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config.GetConnectionString("DefaultConnection"), opt =>
            {
                opt.MigrationsHistoryTable("_GlobleMigrationHistory", "base");
            });
        }

        //Tables

        public DbSet<House> Houses { get; set; } = houses;
        public DbSet<HousePhoto> HousePhotos { get; set; } = housePhotos;
    }
}