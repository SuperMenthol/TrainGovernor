namespace Infrastructure.Entity.TrainGovernor
{
    public class NeighbouringTrainStation
    {
        public int Id { get; set; }
        public int StationId { get; set; }
        public int NeighbourId { get; set; }
        public decimal DistanceInKm { get; set; }
        public bool IsActive { get; set; }

        public TrainStation Station { get; set; }
        public TrainStation NeighbourStation { get; set; }
    }
}