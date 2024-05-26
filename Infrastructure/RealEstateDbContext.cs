using Microsoft.EntityFrameworkCore;
using estatedocflow.api.Models.Entities;
using Document = estatedocflow.api.Models.Entities.Document;

namespace estatedocflow.api.Infrastructure
{
    public class RealEstateDbContext(DbContextOptions<RealEstateDbContext> options, IConfiguration config)
        : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"), opt =>
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