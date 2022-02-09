using AutoMapper;
using Domain.Interfaces.Controllers;
using Domain.Models.Dto;
using Domain.Models.ValueObjects;
using Domain.Models.ValueObjects.Primitives;
using Infrastructure.Interfaces.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    public class LineStartTimeController : Controller, ILineStartTimeController
    {
        ITrainGovernorContext _context;
        ILineController _lineController;
        ILogger<LineStartTimeController> _logger;
        IMapper _mapper;

        public LineStartTimeController(ITrainGovernorContext context, ILineController lineController, 
            ILogger<LineStartTimeController> logger, IMapper mapper)
        {
            _context = context;
            _lineController = lineController;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetForLine/{lineId}/{activeOnly}")]
        public async Task<List<LineStartTimeDto>> GetLineStartTimesForLine(int lineId, bool activeOnly)
        {
            var startTimes = await _context.LineStartTimes.AsNoTracking().Where(x => x.LineId == lineId).ToListAsync();
            if (activeOnly)
            {
                startTimes = startTimes.Where(x => x.IsActive).ToList();
            }

            var res = new List<LineStartTimeDto>();
            foreach (var time in startTimes)
            {
                res.Add(_mapper.Map<LineStartTimeDto>(time));
            }

            return res;
        }

        [HttpGet]
        [Route("GetTimetableData/{lineId}")]
        public LineWithStartTimes GetTimetableData(int startTimeId)
        {
            var startObj = _context.LineStartTimes.Where(x => x.Id == startTimeId)
                .Include(y => y.Line)
                .ThenInclude(z => z.LineStations)
                .ThenInclude(a => a.NeighbouringTrainStation)
                .First();

            var dto = _mapper.Map<LineStartTimeDto>(startObj);
            var relations = dto.Line.LineStations;

            var stationsUsed = _context.Stations
                .Join(relations
                , y => y.Id
                , x => x.NeighbouringTrainStation.StationId
                , (x, y) => x)
                .Join(relations
                , y => y.Id
                , x => x.NeighbouringTrainStation.NeighbourId
                , (x, y) => x);

            var res = new List<StationTime>();
            res.Add(new StationTime(dto.Hour, dto.Minute)
            {
                StationId = relations[0].StationId,
                StationName = stationsUsed.Where(x => x.Id == relations[0].StationId).First().Name,
            });
            for (int i = 1; i < dto.Line.LineStations.Count; i++)
            {
                var hourOfArrival = res.Last().ArrivalTime.AddMinutes(relations[i].GetTimeToArrive);
                var hourOfDeparture = res.Last().DepartureTime.AddMinutes(relations[i].GetTimeToDepart);

                res.Add(new StationTime(hourOfArrival.Hour, hourOfArrival.Minute, relations[i].BreakInMinutes)
                {
                    StationId = relations[i-1].NeighbouringTrainStation.NeighbourId,
                    StationName = stationsUsed.Where(x => x.Id == relations[i-1].NeighbouringTrainStation.NeighbourId).First().Name
                });
            }

            return new LineWithStartTimes()
            {
                LineId = startObj.LineId,
                Collection = res
            };
        }
    }
}