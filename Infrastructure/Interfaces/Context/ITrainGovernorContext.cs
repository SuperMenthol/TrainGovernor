using Microsoft.EntityFrameworkCore;
using Infrastructure.Entity.TrainGovernor;

namespace Infrastructure.Interfaces.Context
{
    public interface ITrainGovernorContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<TrainStation> Stations { get; set; }
        public DbSet<NeighbouringTrainStation> NeighbouringStations { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<LineStation> LineStations { get; set; }
        public int SaveChanges();
    }
}