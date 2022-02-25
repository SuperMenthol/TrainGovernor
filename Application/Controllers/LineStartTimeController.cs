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
    [Route("[controller]")]
    public class LineStartTimeController : Controller, ILineStartTimeController
    {
        ITrainGovernorContext _context;
        ILogger<LineStartTimeController> _logger;
        IMapper _mapper;

        public LineStartTimeController(ITrainGovernorContext context, ILineController lineController, 
            ILogger<LineStartTimeController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetForLine/{lineId}/{activeOnly}")]
        public async Task<List<LineStartTimeDto>> GetLineStartTimesForLine(int lineId, bool activeOnly)
        {
            var startTimes = await _context.LineStartTimes.AsNoTracking().Where(x => x.LineId == lineId)
                .Select(x => _mapper.Map<LineStartTimeDto>(x))
                .ToListAsync();

            if (activeOnly)
            {
                startTimes = startTimes.Where(x => x.IsActive).ToList();
            }

            return startTimes;
        }

        [HttpGet]
        [Route("GetTimetableData/{lineId}")]
        public LineWithStartTimes GetTimetableData([FromRoute] int lineId)
        {
            try
            {
                var startObj = _context.LineStartTimes.Where(x => x.LineId == lineId)
                    .Include(y => y.Line)
                    .ThenInclude(z => z.LineStations)
                    .ThenInclude(a => a.NeighbouringTrainStation)
                    .Select(x => _mapper.Map<LineStartTimeDto>(x))
                    .ToList();

                startObj = startObj.OrderBy(x => x.Hour).ThenBy(x => x.Minute).ToList();

                var relations = startObj[0].Line.LineStations.OrderBy(x => x.StationOrder).ToList();

                var stationIds = relations.Select(x => x.NeighbouringTrainStation.StationId).ToList();
                stationIds.AddRange(relations.Select(x => x.NeighbouringTrainStation.NeighbourId).ToList());

                var stationsUsed = _context.Stations
                    .AsEnumerable()
                    .Join(stationIds
                        , y => y.Id
                        , x => x
                        , (x, y) => x)
                    .Distinct()
                    .ToList();

                var ungroupedCollection = new List<StationTime>();

                foreach (var dto in startObj)
                {
                    var res = new List<StationTime>();
                    res.Add(new StationTime(hour: dto.Hour, minute: dto.Minute)
                    {
                        StationId = relations[0].StationId,
                        StationName = stationsUsed.Where(x => x.Id == relations[0].StationId).First().Name,
                    });
                    for (int i = 0; i < dto.Line.LineStations.Count; i++)
                    {
                        int timeOfArrival = res.Last().ArrivalTimeInMinutes + (int)Math.Floor(relations[i].GetTimeToArrive);
                        var timeOfDeparture = timeOfArrival + relations[i].BreakInMinutes;

                        res.Add(new StationTime(timeOfArrival, relations[i].BreakInMinutes)
                        {
                            StationId = relations[i].NeighbouringTrainStation.NeighbourId,
                            StationName = stationsUsed.Where(x => x.Id == relations[i].NeighbouringTrainStation.NeighbourId).First().Name
                        });
                    }

                    ungroupedCollection.AddRange(res);
                }

                var groupedCollection = ungroupedCollection.GroupBy(x => x.StationName).ToList();

                return new LineWithStartTimes()
                {
                    LineId = startObj[0].Line.Id,
                    LineName = startObj[0].Line.Name,
                    Collection = groupedCollection
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new LineWithStartTimes();
            }
        }
    }
}