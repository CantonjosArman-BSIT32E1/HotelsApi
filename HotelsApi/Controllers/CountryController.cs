using HotelsApi.Services;
using HotelsApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await countryService.GetAllCountries();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById([FromRoute] int id)
        {
            var country = await countryService.GetCountryById(id);
            return Ok(country);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryModel country)
        {
            var createdCountry = await countryService.CreateCountry(country);
            return Ok(createdCountry);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry([FromRoute] int id, [FromBody] UpdateCountryModel country)
        {
            var updatedCountry = await countryService.UpdateCountry(country, id);
            return Ok(updatedCountry);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry([FromRoute] int id)  
        {
            var result = await countryService.DeleteCountry(id);
            if (result)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
