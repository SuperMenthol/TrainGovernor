﻿using AutoMapper;
using Infrastructure.Interfaces.Controllers;
using Infrastructure.Models.Dto;
using Infrastructure.Models.ValueObjects;
using Domain.Interfaces.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    [Route("[controller]")]
    public class TrainStationController : Controller, ITrainStationController
    {
        private ITrainGovernorContext _context;
        private ILogger<TrainStationController> _logger;
        private IMapper _mapper;

        private static object RESULT_STATION_SUCCESS = new
        {
            Success = true,
            Message = "Station has been saved"
        };
        private static object RESULT_STATION_EXISTS = new
        {
            Success = false,
            Message = "This station already exists"
        };
        private static object RESULT_STATION_FAILURE = new
        {
            Success = false,
            Message = "Saving station was not possible"
        };

        public TrainStationController(ITrainGovernorContext context, ILogger<TrainStationController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllStations")]
        public async Task<List<TrainStationDto>> GetAll()
        {
            try
            {
                var stations = await _context.Stations.ToListAsync();
                return stations.Select(station => new TrainStationDto(station)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new List<TrainStationDto>();
            }
        }

        [HttpGet]
        [Route("GetStations/{cityId}")]
        public async Task<List<TrainStationDto>> GetStationsForCity(int cityId)
        {
            try
            {
                var stations = await _context.Stations.Where(x => x.CityId == cityId).ToListAsync();
                return stations.Select(x => new TrainStationDto(x)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new List<TrainStationDto>();
            }
        }

        [HttpGet]
        [Route("GetStation/{stationId}")]
        public async Task<TrainStationDto> GetStation(int id)
        {
            try
            {
                var station = await _context.Stations.Where(x => x.Id == id).FirstOrDefaultAsync();
                return new TrainStationDto(station);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        [HttpGet]
        [Route("GetNeighbours/{stationId}")]
        public async Task<List<NeighbouringTrainStationDto>> GetNeighbouringTrainStations(int stationId)
        {
            try
            {
                var stations = await _context.NeighbouringStations
                    .Include(x => x.Station)
                    .Include(x => x.NeighbourStation)
                    .Where(x => x.StationId == stationId)
                    .Select(x => _mapper.Map<NeighbouringTrainStationDto>(x))
                    .ToListAsync();

                return stations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        [HttpGet]
        [Route("AllNeighbouringStations")]
        public async Task<List<NeighbouringTrainStationDto>> GetAllNeighbouringStations()
        {
            try
            {
                var stations = await _context.NeighbouringStations
                    .Include(x => x.Station)
                    .Include(x => x.NeighbourStation)
                    .Select(x => _mapper.Map<NeighbouringTrainStationDto>(x))
                    .ToListAsync();

                return stations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        [HttpPost]
        [Route("Add")]
        public ActionResult AddStation([FromBody] TrainStationDto trainStationDto)
        {
            try
            {
                if (CheckIfStationExists(trainStationDto))
                {
                    return new JsonResult(RESULT_STATION_EXISTS);
                }

                _context.Stations.Add(trainStationDto.ToEntity());
                _context.SaveChanges();

                return new JsonResult(RESULT_STATION_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new JsonResult(RESULT_STATION_FAILURE);
            }
        }

        [HttpPost]
        [Route("AddNeighbouringStation")]
        public ActionResult<bool> AddNeighbouringStations([FromBody] List<NeighbouringStation> dto)
        {
            try
            {
                foreach (var trainStation in dto)
                {
                    var ent = trainStation.ToEntity();
                    _context.NeighbouringStations.Add(ent);
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return false;
            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateStation([FromBody] TrainStationDto trainStationDto)
        {
            try
            {
                _context.Stations.Update(trainStationDto.ToEntity());
                _context.SaveChanges();
                return new JsonResult(RESULT_STATION_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new JsonResult(RESULT_STATION_FAILURE);
            }
        }

        [HttpPut]
        [Route("UpdateNeighbouringStations")]
        public ActionResult<bool> UpdateNeighbouringStations([FromBody] List<NeighbouringStation> neighbouringStations)
        {
            try
            {
                foreach (var trainStation in neighbouringStations)
                {
                    var recordId = _context.NeighbouringStations.Where(x => x.StationId == trainStation.StationId && x.NeighbourId == trainStation.NeighbourId).AsNoTracking().First().Id;

                    var entity = trainStation.ToEntity();
                    entity.Id = recordId;

                    _context.NeighbouringStations.Update(entity);
                    _context.SaveChanges();
                }

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        private bool CheckIfStationExists(TrainStationDto dto)
        {
            return _context.Stations.Any(x => x.Name == dto.Name)
                || (_context.Stations.Any(x => x.Address == dto.Address.StreetAddress
                && x.CityId == dto.CityId));
        }
    }
}