﻿// <auto-generated />
using Domain.Context.TrainGovernorContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Domain.Migrations
{
    [DbContext(typeof(TrainGovernorContext))]
    [Migration("20220202080627_Line-LineStation")]
    partial class LineLineStation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Infrastructure.Entity.TrainGovernor.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Infrastructure.Entity.TrainGovernor.Line", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lines");
                });

            modelBuilder.Entity("Infrastructure.Entity.TrainGovernor.LineStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("AvgSpeed")
                        .HasColumnType("real");

                    b.Property<int>("BreakInMinutes")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("LineId")
                        .HasColumnType("int");

                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.Property<int>("StationOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LineId");

                    b.HasIndex("StationId");

                    b.ToTable("LineStations");
                });

            modelBuilder.Entity("Infrastructure.Entity.TrainGovernor.NeighbouringTrainStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("DistanceInKm")
                        .HasColumnType("real");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("NeighbourId")
                        .HasColumnType("int");

                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NeighbourId");

                    b.HasIndex("StationId");

                    b.ToTable("NeighbouringStations");
                });

            modelBuilder.Entity("Infrastructure.Entity.TrainGovernor.TrainStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("Infrastructure.Entity.TrainGovernor.LineStation", b =>
                {
                    b.HasOne("Infrastructure.Entity.TrainGovernor.Line", "Line")
                        .WithMany("LineStations")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entity.TrainGovernor.TrainStation", "TrainStation")
                        .WithMany("LinesOfStation")
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Line");

                    b.Navigation("TrainStation");
                });

            modelBuilder.Entity("Infrastructure.Entity.TrainGovernor.NeighbouringTrainStation", b =>
                {
                    b.HasOne("Infrastructure.Entity.TrainGovernor.TrainStation", "NeighbourStation")
                        .WithMany("NeighbouringTrainStations")
                        .HasForeignKey("NeighbourId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entity.TrainGovernor.TrainStation", "Station")
                        .WithMany("NeighbourTrainStations")
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("NeighbourStation");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("Infrastructure.Entity.TrainGovernor.TrainStation", b =>
                {
                    b.HasOne("Infrastructure.Entity.TrainGovernor.City", "City")
                        .WithMany("Stations")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Infrastructure.Entity.TrainGovernor.City", b =>
                {
                    b.Navigation("Stations");
                });

            modelBuilder.Entity("Infrastructure.Entity.TrainGovernor.Line", b =>
                {
                    b.Navigation("LineStations");
                });

            modelBuilder.Entity("Infrastructure.Entity.TrainGovernor.TrainStation", b =>
                {
                    b.Navigation("LinesOfStation");

                    b.Navigation("NeighbourTrainStations");

                    b.Navigation("NeighbouringTrainStations");
                });
#pragma warning restore 612, 618
        }
    }
}
