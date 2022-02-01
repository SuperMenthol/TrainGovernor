using AutoMapper;
using Domain.Interfaces.Controllers;
using Domain.Models.Dto;
using Infrastructure.Interfaces.Context;
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
                    .Where(x => x.StationId == stationId).ToListAsync();
                var res = new List<NeighbouringTrainStationDto>();
                foreach (var station in stations)
                {
                    res.Add(_mapper.Map<NeighbouringTrainStationDto>(station));
                }

                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        [HttpPost]
        [Route("Add")]
        public ActionResult<bool> AddStation([FromBody] TrainStationDto trainStationDto)
        {
            try
            {
                _context.Stations.Add(trainStationDto.ToEntity());
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult<bool> UpdateStation([FromBody] TrainStationDto trainStationDto)
        {
            try
            {
                _context.Stations.Update(trainStationDto.ToEntity());
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }
    }
}