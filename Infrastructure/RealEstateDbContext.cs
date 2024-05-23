using Microsoft.EntityFrameworkCore;
using estatedocflow.api.Models.Entities;
using System.Reflection.Metadata;
using Document = estatedocflow.api.Models.Entities.Document;

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
        public DbSet<House> Houses { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<HouseAttachment> HouseAttachments { get; set; }
    }
}