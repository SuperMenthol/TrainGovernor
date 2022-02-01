﻿using Domain.Models.Dto;
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

        [HttpPut]
        [Route("Update")]
        ActionResult<bool> UpdateStation([FromBody] TrainStationDto trainStationDto);
    }
}
