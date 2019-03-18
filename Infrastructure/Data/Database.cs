﻿using Infrastructure.DTOs;
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

            modelBuilder.
                Entity<AnalysisDTO>()
                .HasKey(x => x.Id);
        }
    }
}
