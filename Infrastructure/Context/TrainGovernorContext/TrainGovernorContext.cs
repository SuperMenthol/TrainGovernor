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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=ASP_TrainGovernor; Database=DESKTOP-NPC8O49\\SOL; Trusted_Connection=True; MultipleActiveResultSets=true");
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

            modelBuilder.Entity<TrainStation>()
                .HasOne(x => x.City)
                .WithMany(x => x.Stations);
        }

        public override int SaveChanges() => base.SaveChanges();
    }
}