using HotelsApi.Context;
using HotelsApi.Services;
using HotelsApi.Dtos;
using HotelsApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await cityService.GetAllCities();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById([FromRoute] int id)
        {
            var city = await cityService.GetCityById(id);
            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CreateCityModel city)
        {
            var createdCity = await cityService.CreateCity(city);
            return Ok(createdCity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity([FromRoute] int id, [FromBody] UpdateCityModel city)
        {
            var updatedCity = await cityService.UpdateCity(city, id);
            return Ok(updatedCity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity([FromRoute] int id)
        {
            var result = await cityService.DeleteCity(id);
            if (result)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
