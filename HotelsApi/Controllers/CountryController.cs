using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CountryController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Country>> GetAllCountries()
        {
            return await _context.Country.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountryById(int id)
        {
            var country = await _context.Country.FindAsync(id);
            if (country == null) return NotFound();
            return country;
        }

        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountry([FromBody] Country country)
        {
            _context.Country.Add(country);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCountryById), new { id = country.CountryId }, country);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] Country country)
        {
            var existingCountry = await _context.Country.FindAsync(id);
            if (existingCountry == null) return NotFound();

            existingCountry.CountryCode = country.CountryCode;
            existingCountry.CountryName = country.CountryName;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Country.FindAsync(id);
            if (country == null) return NotFound();

            _context.Country.Remove(country);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
