﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(Database))]
    partial class DatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Infrastructure.DTOs.AnalysisDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<Guid?>("EndLocationId");

                    b.Property<TimeSpan>("Every");

                    b.Property<Guid?>("StartLocationId");

                    b.Property<int>("Status");

                    b.Property<TimeSpan>("TimeFrom");

                    b.Property<TimeSpan>("TimeTo");

                    b.Property<string>("Weekdays");

                    b.HasKey("Id");

                    b.HasIndex("EndLocationId");

                    b.HasIndex("StartLocationId");

                    b.ToTable("Analysis");
                });

            modelBuilder.Entity("Infrastructure.DTOs.LocationDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("Infrastructure.DTOs.PriceEstimateDTO", b =>
                {
                    b.Property<Guid>("AnalysisId");

                    b.Property<string>("ProductId");

                    b.Property<DateTime>("Date");

                    b.Property<float>("HighEstimate");

                    b.Property<float>("LowEstimate");

                    b.HasKey("AnalysisId", "ProductId", "Date");

                    b.ToTable("PriceEstimate");
                });

            modelBuilder.Entity("Infrastructure.DTOs.AnalysisDTO", b =>
                {
                    b.HasOne("Infrastructure.DTOs.LocationDTO", "EndLocation")
                        .WithMany()
                        .HasForeignKey("EndLocationId");

                    b.HasOne("Infrastructure.DTOs.LocationDTO", "StartLocation")
                        .WithMany()
                        .HasForeignKey("StartLocationId");
                });

            modelBuilder.Entity("Infrastructure.DTOs.PriceEstimateDTO", b =>
                {
                    b.HasOne("Infrastructure.DTOs.AnalysisDTO", "Analysis")
                        .WithMany("Prices")
                        .HasForeignKey("AnalysisId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
