using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public HotelsController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Hotels>> GetAllHotels()
        {
            return await _context.Hotels.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotels>> GetHotelById(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return NotFound();
            return hotel;
        }

        [HttpPost]
        public async Task<ActionResult<Hotels>> CreateHotel([FromBody] Hotels hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.HotelId }, hotel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] Hotels hotel)
        {
            var existingHotel = await _context.Hotels.FindAsync(id);
            if (existingHotel == null) return NotFound();

            existingHotel.HotelCode = hotel.HotelCode;
            existingHotel.HotelName = hotel.HotelName;
            existingHotel.HotelDescription = hotel.HotelDescription;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return NotFound();

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}