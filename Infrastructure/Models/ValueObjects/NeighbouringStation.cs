using Domain.Entity.TrainGovernor;

namespace Infrastructure.Models.ValueObjects
{
    public class NeighbouringStation
    {
        public int Id { get; set; }
        public int StationId { get; set; }
        public int NeighbourId { get; set; }
        public float DistanceInKm { get; set; }
        public bool IsActive { get; set; }

        public NeighbouringStation()
        {

        }

        public NeighbouringTrainStation ToEntity()
        {
            return new NeighbouringTrainStation()
            {
                StationId = StationId,
                NeighbourId = NeighbourId,
                DistanceInKm = DistanceInKm,
                IsActive = IsActive
            };
        }
    }
}
