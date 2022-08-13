using Infrastructure.Models.Dto;
using Infrastructure.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Interfaces.Controllers
{
    public interface ITrainStationController
    {
        Task<List<TrainStationDto>> GetAll();
        public Task<List<TrainStationDto>> GetStationsForCity([FromRoute] int cityId);
        Task<TrainStationDto> GetStation(int id);
        Task<List<NeighbouringTrainStationDto>> GetNeighbouringTrainStations(int stationId);
        Task<List<NeighbouringTrainStationDto>> GetAllNeighbouringStations();
        ActionResult AddStation([FromBody] TrainStationDto trainStationDto);
        ActionResult UpdateStation([FromBody] TrainStationDto trainStationDto);
        ActionResult<bool> AddNeighbouringStations([FromBody] List<NeighbouringStation> dto);
        ActionResult<bool> UpdateNeighbouringStations([FromBody] List<NeighbouringStation> neighbouringStations);
    }
}