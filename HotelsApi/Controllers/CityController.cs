using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CityController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<City>> GetAllCities()
        {
            return await _context.City.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCityById(int id)
        {
            var city = await _context.City.FindAsync(id);
            if (city == null) return NotFound();
            return city;
        }

        [HttpPost]
        public async Task<ActionResult<City>> CreateCity([FromBody] City city)
        {
            _context.City.Add(city);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCityById), new { id = city.CityId }, city);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] City city)
        {
            var existingCity = await _context.City.FindAsync(id);
            if (existingCity == null) return NotFound();

            existingCity.CityCode = city.CityCode;
            existingCity.CityName = city.CityName;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _context.City.FindAsync(id);
            if (city == null) return NotFound();

            _context.City.Remove(city);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
