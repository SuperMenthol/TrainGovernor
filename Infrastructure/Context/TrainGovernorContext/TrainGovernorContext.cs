using Microsoft.EntityFrameworkCore;
using Infrastructure.Entity.TrainGovernor;
using Infrastructure.Interfaces.Context;

namespace Infrastructure.Context.TrainGovernorContext
{
    public class TrainGovernorContext : DbContext, ITrainGovernorContext
    {
        public TrainGovernorContext()
            : base()
        {
        }

        public TrainGovernorContext(DbContextOptions<TrainGovernorContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<TrainStation> Stations { get; set; }
        public DbSet<NeighbouringTrainStation> NeighbouringStations { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<LineStation> LineStations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<City>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TrainStation>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<TrainStation>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<NeighbouringTrainStation>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<NeighbouringTrainStation>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Line>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Line>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<LineStation>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<LineStation>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TrainStation>()
                .HasOne(x => x.City)
                .WithMany(x => x.Stations);

            modelBuilder.Entity<NeighbouringTrainStation>()
                .HasOne(x => x.Station)
                .WithMany(y => y.NeighbourTrainStations)
                .HasForeignKey(x => x.StationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NeighbouringTrainStation>()
                .HasOne(x => x.NeighbourStation)
                .WithMany(y => y.NeighbouringTrainStations)
                .HasForeignKey(x => x.NeighbourId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LineStation>()
                .HasOne(x => x.Line)
                .WithMany(y => y.LineStations)
                .HasForeignKey(x => x.LineId);

            modelBuilder.Entity<LineStation>()
                .HasOne(x => x.TrainStation)
                .WithMany(y => y.LinesOfStation)
                .HasForeignKey(x => x.StationId);

            modelBuilder.Entity<LineStation>()
                .HasOne(x => x.NeighbouringTrainStation)
                .WithMany(y => y.StationsInLines)
                .HasForeignKey(x => x.NeighbourRelationId);
        }

        public override int SaveChanges() => base.SaveChanges();
    }
}