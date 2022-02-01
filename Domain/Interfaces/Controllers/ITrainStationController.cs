using Domain.Models.Dto;
using Domain.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces.Controllers
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

        [HttpPost]
        [Route("Add")]
        ActionResult<bool> AddStation([FromBody] TrainStationDto trainStationDto);

        [HttpPost]
        [Route("AddNeighbouringStation")]
        ActionResult<bool> AddNeighbouringStations([FromBody] List<NeighbouringStation> dto);

        [HttpPut]
        [Route("Update")]
        ActionResult<bool> UpdateStation([FromBody] TrainStationDto trainStationDto);

        [HttpPut]
        [Route("UpdateNeighbouringStations")]
        ActionResult<bool> UpdateNeighbouringStations([FromBody] List<NeighbouringStation> neighbouringStations);
    }
}
