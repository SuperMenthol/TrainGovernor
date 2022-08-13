using Infrastructure.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Interfaces.Controllers
{
    public interface ICityController
    {
        Task<List<CityOverviewDto>> GetCities();

        Task<CityOverviewDto> GetById(int id);
        ActionResult UpdateCity([FromBody] CityOverviewDto city);
        ActionResult AddCity(string name, string? postCode = null);
    }
}