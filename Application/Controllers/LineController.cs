using AutoMapper;
using Domain.Interfaces.Controllers;
using Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Infrastructure.Interfaces.Context;

namespace Application.Controllers
{
    [Route("[Controller]")]
    public class LineController : Controller, ILineController
    {
        private ITrainGovernorContext _context;
        private readonly ILogger _logger;
        private IMapper _mapper;

        public LineController(ITrainGovernorContext context, ILogger<LineController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllLines")]
        public async Task<List<LineDto>> GetAllLines()
        {
            try
            {
                var lines = await _context.Lines
                    .Include(x => x.LineStations)
                    .ThenInclude(y => y.TrainStation)
                    .Include(x => x.LineStations)
                    .ThenInclude(y => y.NeighbouringTrainStation)
                    .ThenInclude(y => y.NeighbourStation)
                    .Select(x => _mapper.Map<LineDto>(x))
                    .ToListAsync();

                return lines;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        [HttpGet]
        [Route("GetWithStartTimes")]
        public List<LineDto> GetLinesHavingStartTimes()
        {
            try
            {
                var lines = _context.Lines
                    .Where(x => x.StartTimes.Count > 0)
                    .Include(x => x.LineStations)
                    .ThenInclude(y => y.TrainStation)
                    .Include(x => x.LineStations)
                    .ThenInclude(y => y.NeighbouringTrainStation)
                    .ThenInclude(y => y.NeighbourStation)
                    .Select(x => _mapper.Map<LineDto>(x))
                    .ToList();

                var res = new List<LineDto>();
                foreach (var line in lines)
                {
                    res.Add(_mapper.Map<LineDto>(line));
                }
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        [HttpGet]
        [Route("GetLine/{id}")]
        public LineDto GetLine(int id)
        {
            try
            {
                var line = _context.Lines.Where(x => x.Id == id)
                    .Include(x => x.LineStations)
                    .ThenInclude(y => y.TrainStation)
                    .Include(x => x.LineStations)
                    .ThenInclude(y => y.NeighbouringTrainStation)
                    .ThenInclude(y => y.NeighbourStation)
                    .FirstOrDefault();

                if (line != null)
                {
                    return _mapper.Map<LineDto>(line);
                }
                throw new Exception("Line with this ID was not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        [HttpPost]
        [Route("Add")]
        public async Task AddLine([FromBody] LineDto line)
        {
            try
            {
                var lineEntity = line.ToEntity();
                _context.Lines.Add(lineEntity);
                _context.SaveChanges();

                foreach (var item in line.LineStations)
                {
                    item.LineId = lineEntity.Id;
                    var itemEnt = item.ToEntity();
                    _context.LineStations.Add(item.ToEntity());
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task UpdateLine([FromBody] LineDto line)
        {
            try
            {
                var lineEntity = line.ToEntity();
                _context.Lines.Update(lineEntity);
                foreach (var item in line.LineStations)
                {
                    item.LineId = lineEntity.Id;
                    var stationEntity = item.ToEntity();
                    stationEntity.LineId = lineEntity.Id;
                    _context.LineStations.Update(stationEntity);
                }
                foreach (var item in line.StartTimes)
                {
                    item.LineId = lineEntity.Id;
                    var lineTimeEntity = item.ToEntity();
                    lineTimeEntity.LineId = lineEntity.Id;
                    _context.LineStartTimes.Update(lineTimeEntity);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}