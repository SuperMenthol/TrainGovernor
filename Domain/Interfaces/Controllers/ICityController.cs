﻿using Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces.Controllers
{
    public interface ICityController
    {
        Task<List<CityOverviewDto>> GetCities();
        Task<CityOverviewDto> GetById(int id);

        [HttpPut]
        [Route("UpdateCity")]
        ActionResult<bool> UpdateCity([FromBody] CityOverviewDto city);

        [HttpPost]
        [Route("Add/{name}/{postCode?}")]
        ActionResult<bool> AddCity(string name, string? postCode = null);
    }
}