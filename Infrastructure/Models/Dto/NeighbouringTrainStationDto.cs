using Domain.Entity.TrainGovernor;

namespace Infrastructure.Models.Dto
{
    public class NeighbouringTrainStationDto
    {
        public int Id { get; set; }
        public int StationId { get; set; }
        public string StationName { get; set; }
        public int NeighbourId { get; set; }
        public float DistanceInKm { get; set; }
        public bool IsActive { get; set; }
        public TrainStationDto NeighbourStation { get; set; }

        public NeighbouringTrainStationDto(NeighbouringTrainStation entity = null)
        {
            if (entity != null)
            {
                Id = entity.Id;
                StationId = entity.StationId;
                NeighbourId = entity.NeighbourId;
                DistanceInKm = entity.DistanceInKm;
                IsActive = entity.IsActive;

                StationName = entity.Station.Name;
            }
        }
    }
}