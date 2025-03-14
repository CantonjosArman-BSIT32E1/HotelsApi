using HotelsApi.Services;
using HotelsApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService hotelService;

        public HotelsController(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await hotelService.GetAllHotels();
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById([FromRoute] int id)
        {
            var hotel = await hotelService.GetHotelById(id);
            return Ok(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelModel hotel)
        {
            var createdHotel = await hotelService.CreateHotel(hotel);
            return Ok(createdHotel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel([FromRoute] int id, [FromBody] UpdateHotelModel hotel)
        {
            var updatedHotel = await hotelService.UpdateHotel(hotel, id);
            return Ok(updatedHotel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel([FromRoute] int id)
        {
            var result = await hotelService.DeleteHotel(id);
            if (result)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
