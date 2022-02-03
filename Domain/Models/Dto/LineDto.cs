using Infrastructure.Entity.TrainGovernor;

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
        public string TotalTravelTimeString
        {
            get
            {
                return TimeSpan.FromMinutes(TotalTravelTime).ToString("hh':'mm':'ss");
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
                return LineStations != null ? LineStations
                    .Where(x => x.StationOrder == 1)
                    .FirstOrDefault()?
                    .TrainStation :
                    null;
            }
        }

        public TrainStationDto? EndingStation
        {
            get
            {
                return LineStations.Count > 0 ?
                    LineStations
                    .OrderByDescending(x => x.StationOrder)
                    .FirstOrDefault()?
                    .NeighbouringTrainStation?
                    .NeighbourStation :
                    null;
            }
        }

        public LineDto()
        {

        }

        public Line ToEntity()
        {
            var lines = new List<LineStation>();
            LineStations.ForEach(x => lines.Add(x.ToEntity()));

            return new Line()
            {
                Id = Id,
                Name = Name,
                IsActive = IsActive,
                //LineStations = lines
            };
        }
    }
}