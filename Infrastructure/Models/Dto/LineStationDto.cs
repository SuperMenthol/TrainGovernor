using Domain.Entity.TrainGovernor;

namespace Infrastructure.Models.Dto
{
    public class LineStationDto
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public int StationId { get; set; }
        public int NeighbourRelationId { get; set; }
        public int StationOrder { get; set; }
        public int BreakInMinutes { get; set; }
        public float AvgSpeed { get; set; }
        public bool IsActive { get; set; }
        public TrainStationDto TrainStation { get; set; }
        public NeighbouringTrainStationDto NeighbouringTrainStation { get; set; }

        public float GetTimeToArrive { get { return NeighbouringTrainStation != null ? 60 * (NeighbouringTrainStation.DistanceInKm / AvgSpeed) : 0f; } }
        public float GetTimeToDepart { get { return GetTimeToArrive + BreakInMinutes; } }
        public string TimeToArriveString { get { return TimeSpan.FromMinutes(GetTimeToArrive).ToString("hh':'mm':'ss"); } }
        public string TimeToDepartString { get { return TimeSpan.FromMinutes(GetTimeToDepart).ToString("hh':'mm':'ss"); } }

        public LineStationDto()
        {

        }

        public LineStation ToEntity()
        {
            return new LineStation()
            {
                Id = Id,
                LineId = LineId,
                StationId = StationId,
                StationOrder = StationOrder,
                BreakInMinutes = BreakInMinutes,
                AvgSpeed = AvgSpeed,
                NeighbourRelationId = NeighbourRelationId,
                IsActive = IsActive
            };
        }
    }
}