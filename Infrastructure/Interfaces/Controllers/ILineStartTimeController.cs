using Infrastructure.Models.Dto;
using Infrastructure.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Interfaces.Controllers
{
    public interface ILineStartTimeController
    {
        [HttpGet]
        [Route("GetForLine/{lineId}/{active}")]
        Task<List<LineStartTimeDto>> GetLineStartTimesForLine(int lineId, bool activeOnly);

        [HttpGet]
        [Route("GetTimetableData")]
        LineWithStartTimes GetTimetableData([FromRoute] int lineId);
    }
}