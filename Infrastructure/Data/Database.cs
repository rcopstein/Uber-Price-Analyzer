using Infrastructure.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options)
            : base(options)
        {
        }

        public DbSet<AnalysisDTO> Analyses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<AnalysisDTO>()
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<AnalysisDTO>()
                .HasMany(x => x.Prices);

            modelBuilder
                .Entity<AnalysisDTO>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<LocationDTO>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<PriceEstimateDTO>()
                .HasKey(nameof(PriceEstimateDTO.AnalysisId), 
                        nameof(PriceEstimateDTO.ProductId),
                        nameof(PriceEstimateDTO.Date));

            modelBuilder
                .Entity<PriceEstimateDTO>()
                .HasOne(x => x.Analysis);
        }
    }
}
