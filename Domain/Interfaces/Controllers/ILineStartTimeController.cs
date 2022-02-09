using Domain.Models.Dto;
using Domain.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Domain.Interfaces.Controllers
{
    public interface ILineStartTimeController
    {
        [HttpGet]
        [Route("GetForLine/{lineId}/{active}")]
        Task<List<LineStartTimeDto>> GetLineStartTimesForLine(int lineId, bool activeOnly);

        [HttpGet]
        [Route("GetTimetableData/{lineId}")]
        LineWithStartTimes GetTimetableData(int lineId);
    }
}