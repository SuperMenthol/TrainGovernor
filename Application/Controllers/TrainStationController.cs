using Domain.Interfaces.Controllers;
using Domain.Models.Dto;
using Infrastructure.Interfaces.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers
{
    public class TrainStationController : Controller, ITrainStationController
    {
        private ITrainGovernorContext _context;

        public TrainStationController(ITrainGovernorContext context)
        {
            _context = context;
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
            catch
            {
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
            catch
            {
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
            catch
            {
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
            catch
            {
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
            catch
            {
                return false;
            }
        }
    }
}