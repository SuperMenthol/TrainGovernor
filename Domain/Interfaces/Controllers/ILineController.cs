using Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces.Controllers
{
    public interface ILineController
    {
        [HttpGet]
        [Route("GetAllLines")]
        Task<List<LineDto>> GetAllLines();

        [HttpGet]
        [Route("GetLine/{id}")]
        LineDto GetLine(int id);
    }
}
