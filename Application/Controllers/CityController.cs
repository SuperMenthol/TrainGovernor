using Domain.Interfaces.Controllers;
using Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Interfaces.Context;

namespace Application.Controllers
{
    [Route("[controller]")]
    public class CityController : Controller, ICityController
    {
        protected ITrainGovernorContext _context;

        public CityController(ITrainGovernorContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<CityOverviewDto>> GetCities()
        {
            try
            {
                return await _context.Cities
                .Select(x => new CityOverviewDto(x))
                .ToListAsync();
            }
            catch
            {
                return new List<CityOverviewDto>();
            }
        }

        public async Task<CityOverviewDto> GetById(int id)
        {
            try
            {
                var a = await _context.Cities
                .Where(x => x.Id == id).FirstOrDefaultAsync();

                return new CityOverviewDto(a);
            }
            catch
            {
                return null;
            }
        }

        [HttpPut]
        [Route("UpdateCity")]
        public ActionResult<bool> UpdateCity([FromBody] CityOverviewDto city)
        {
            try
            {
                _context.Cities.Update(city.ToEntity());
                _context.SaveChanges();
                return View();
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        [Route("Add/{name}/{postCode?}")]
        public ActionResult<bool> AddCity(string name, string? postCode = null)
        {
            try
            {
                var nCity = new CityOverviewDto()
                {
                    Name = name,
                    ZipCode = postCode,
                    IsActive = true
                };

                _context.Cities.Add(nCity.ToEntity());
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}