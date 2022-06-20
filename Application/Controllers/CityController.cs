using AutoMapper;
using Domain.Interfaces.Controllers;
using Domain.Models.Dto;
using Infrastructure.Interfaces.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    [Route("[controller]")]
    public class CityController : Controller, ICityController
    {
        protected ITrainGovernorContext _context;
        private readonly ILogger<CityController> _logger;
        private IMapper _mapper;

        private static object RESULT_CITY_SUCCESS = new
        {
            Success = true,
            Message = "City has been saved"
        };

        private static object RESULT_CITY_EXISTS = new
        {
            Success = false,
            Message = "This city already exists"
        };

        private static object RESULT_CITY_FAILURE = new
        {
            Success = false,
            Message = "City was not saved due to an error"
        };

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
                var cities = _context.Cities
                    .Include(x => x.Stations)
                    .Select(x => _mapper.Map<CityOverviewDto>(x))
                    .ToList();

                return cities;
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
        public ActionResult UpdateCity([FromBody] CityOverviewDto city)
        {
            try
            {
                if (CheckIfCityExists(city))
                {
                    return new JsonResult(RESULT_CITY_EXISTS);
                }

                _context.Cities.Update(city.ToEntity());
                _context.SaveChanges();
                return new JsonResult(RESULT_CITY_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new JsonResult(RESULT_CITY_FAILURE);
            }
        }

        [HttpPost]
        [Route("Add/{name}/{postCode?}")]
        public ActionResult AddCity(string name, string? postCode)
        {
            try
            {
                var nCity = new CityOverviewDto()
                {
                    Name = name,
                    ZipCode = postCode,
                    IsActive = true
                };

                if (CheckIfCityExists(nCity))
                {
                    return new JsonResult(RESULT_CITY_EXISTS);
                }

                _context.Cities.Add(nCity.ToEntity());
                _context.SaveChanges();
                return new JsonResult(RESULT_CITY_SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new JsonResult(RESULT_CITY_FAILURE);
            }
        }

        private bool CheckIfCityExists(CityOverviewDto dto)
        {
            return _context.Cities.Any(x => x.Name == dto.Name && x.ZipCode == dto.ZipCode);
        }
    }
}