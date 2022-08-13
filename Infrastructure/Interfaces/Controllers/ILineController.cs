using Infrastructure.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Interfaces.Controllers
{
    public interface ILineController
    {
        Task<List<LineDto>> GetAllLines();
        LineDto GetLine(int id);
        List<LineDto> GetLinesHavingStartTimes();
        Task AddLine([FromBody] LineDto line);
        Task UpdateLine([FromBody] LineDto line);
    }
}
