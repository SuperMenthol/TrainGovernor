using Infrastructure.Entity.TrainGovernor;

namespace Domain.Models.ValueObjects
{
    public class NeighbouringStation
    {
        public int Id { get; set; }
        public int StationId { get; set; }
        public int NeighbourId { get; set; }
        public decimal DistanceInKm { get; set; }
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
