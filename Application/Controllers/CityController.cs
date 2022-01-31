using AutoMapper;
using Domain.Interfaces.Controllers;
using Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Infrastructure.Interfaces.Context;

namespace Application.Controllers
{
    [Route("[controller]")]
    public class CityController : Controller, ICityController
    {
        protected ITrainGovernorContext _context;
        private readonly ILogger<CityController> _logger;
        private IMapper _mapper;

        public CityController(ITrainGovernorContext context, ILogger<CityController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<CityOverviewDto>> GetCities()
        {
            try
            {
                var cities = _context.Cities.Include(x => x.Stations);

                var res = new List<CityOverviewDto>();
                foreach (var city in cities)
                {
                    var mappedCity = _mapper.Map<CityOverviewDto>(city);
                    res.Add(_mapper.Map<CityOverviewDto>(city));
                }

                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new List<CityOverviewDto>();
            }
        }

        public async Task<CityOverviewDto> GetById(int id)
        {
            try
            {
                var a = await _context.Cities
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

                return new CityOverviewDto(a);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
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
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
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
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }
    }
}