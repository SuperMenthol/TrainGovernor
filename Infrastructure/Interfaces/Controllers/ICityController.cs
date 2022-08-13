using Infrastructure.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Interfaces.Controllers
{
    public interface ICityController
    {
        Task<List<CityOverviewDto>> GetCities();
        Task<CityOverviewDto> GetById(int id);

        [HttpPut]
        [Route("UpdateCity")]
        ActionResult UpdateCity([FromBody] CityOverviewDto city);

        [HttpPost]
        [Route("Add/{name}/{postCode?}")]
        ActionResult AddCity(string name, string? postCode = null);
    }
}