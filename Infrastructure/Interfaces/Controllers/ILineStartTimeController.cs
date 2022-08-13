using Infrastructure.Models.Dto;
using Infrastructure.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Interfaces.Controllers
{
    public interface ILineStartTimeController
    {
        Task<List<LineStartTimeDto>> GetLineStartTimesForLine(int lineId, bool activeOnly);
        LineWithStartTimes GetTimetableData([FromRoute] int lineId);
    }
}