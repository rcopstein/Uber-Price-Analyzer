using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Infrastructure.DTOs;

namespace Infrastructure.Data
{
    public class Database : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<AnalysisDTO> Analyses { get; set; }

        public Database(
            DbContextOptions<Database> options, 
            IConfiguration configuration)
            : base(options)
        {
            if (System.Diagnostics.Debugger.IsAttached == false)
            {
                System.Diagnostics.Debugger.Launch();
            }

            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var section          = _configuration.GetSection("Database");
            var connectionString = section.GetValue<string>("ConnectionString");
            var database         = section.GetValue<string>("Database");
            var username         = section.GetValue<string>("Username");
            var password         = section.GetValue<string>("Password");
            var host             = section.GetValue<string>("Host");

            var cstr = string.Format(connectionString,
                host, database, username, password);

            optionsBuilder.UseMySql(cstr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<AnalysisDTO>()
                .ToTable("Analysis");

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
                .ToTable("Location");

            modelBuilder
                .Entity<LocationDTO>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<PriceEstimateDTO>()
                .ToTable("PriceEstimate");

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
