using Infrastructure.Models.Dto;
using Infrastructure.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Interfaces.Controllers
{
    public interface ITrainStationController
    {
        [HttpGet]
        [Route("GetAllStations")]
        Task<List<TrainStationDto>> GetAll();

        [HttpGet]
        [Route("GetStations/{cityId}")]
        public Task<List<TrainStationDto>> GetStationsForCity([FromRoute] int cityId);

        [HttpGet]
        [Route("GetStation/{stationId}")]
        Task<TrainStationDto> GetStation(int id);

        [HttpGet]
        [Route("GetNeighbours/{stationId}")]
        Task<List<NeighbouringTrainStationDto>> GetNeighbouringTrainStations(int stationId);

        [HttpGet]
        [Route("AllNeighbouringStations")]
        Task<List<NeighbouringTrainStationDto>> GetAllNeighbouringStations();

        [HttpPost]
        [Route("Add")]
        ActionResult AddStation([FromBody] TrainStationDto trainStationDto);

        [HttpPut]
        [Route("Update")]
        ActionResult UpdateStation([FromBody] TrainStationDto trainStationDto);

        [HttpPost]
        [Route("AddNeighbouringStation")]
        ActionResult<bool> AddNeighbouringStations([FromBody] List<NeighbouringStation> dto);

        [HttpPut]
        [Route("UpdateNeighbouringStations")]
        ActionResult<bool> UpdateNeighbouringStations([FromBody] List<NeighbouringStation> neighbouringStations);
    }
}
