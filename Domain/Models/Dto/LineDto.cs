namespace Domain.Models.Dto
{
    public class LineDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<LineStationDto> LineStations { get; set; }
        public float TotalTravelTime
        {
            get
            {
                var totalTime = 0f;
                if (LineStations != null && LineStations.Count > 0)
                {
                    foreach (var station in LineStations)
                    {
                        totalTime += station.GetTimeToDepart;
                    }
                }

                return totalTime;
            }
        }
        public int AllStations
        {
            get
            {
                if (LineStations != null && LineStations.Count > 0)
                {
                    // add 1 to include final station
                    return LineStations.Count + 1;
                }
                else return 0;
            }
        }

        public TrainStationDto? StartingStation
        {
            get
            {
                return LineStations
                    .Where(x => x.StationOrder == 1)
                    .FirstOrDefault()?
                    .TrainStation;
            }
        }

        public TrainStationDto EndingStation
        {
            get
            {
                return LineStations
                    .OrderByDescending(x => x.StationOrder)
                    .First()
                    .NeighbouringTrainStation
                    .NeighbourStation;
            }
        }

        public LineDto()
        {

        }
    }
}