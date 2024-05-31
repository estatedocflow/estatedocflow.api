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
            var connectionString = config.GetConnectionString("PostgresConnection") ??
                                   config["POSTGRES_CONNECTION_STRING"];
            optionsBuilder.UseNpgsql(connectionString, opt =>
            {
                opt.MigrationsHistoryTable("_GlobeMigrationHistory", "base");
            });
        }

        //Tables
        public DbSet<House> Houses { get; init; } = null!;
        public DbSet<Document> Documents { get; init; } = null!;
        public DbSet<HouseAttachment> HouseAttachments { get; init; } = null!;
    }
}