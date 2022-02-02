namespace Domain.Models.Dto
{
    public class LineStationDto
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public int StationId { get; set; }
        public int StationOrder { get; set; }
        public int BreakInMinutes { get; set; }
        public float AvgSpeed { get; set; }
        public bool IsActive { get; set; }
        public TrainStationDto TrainStation { get; set; }
        public NeighbouringTrainStationDto NeighbouringTrainStation { get; set; }

        public float GetTimeToArrive { get { return 60 * (NeighbouringTrainStation.DistanceInKm / AvgSpeed); } }
        public float GetTimeToDepart { get { return GetTimeToArrive + BreakInMinutes; } }

        public LineStationDto()
        {

        }
    }
}