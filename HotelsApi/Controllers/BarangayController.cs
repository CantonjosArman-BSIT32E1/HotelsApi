using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarangayController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BarangayController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Barangay>> GetAllBarangays()
        {
            return await _context.Barangay.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Barangay>> GetBarangayById(int id)
        {
            var barangay = await _context.Barangay.FindAsync(id);
            if (barangay == null) return NotFound();
            return barangay;
        }

        [HttpPost]
        public async Task<ActionResult<Barangay>> CreateBarangay([FromBody] Barangay barangay)
        {
            _context.Barangay.Add(barangay);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBarangayById), new { id = barangay.BarangayId }, barangay);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBarangay(int id, [FromBody] Barangay barangay)
        {
            var existingBarangay = await _context.Barangay.FindAsync(id);
            if (existingBarangay == null) return NotFound();

            existingBarangay.BarangayName = barangay.BarangayName;
            existingBarangay.PostalCode = barangay.PostalCode;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarangay(int id)
        {
            var barangay = await _context.Barangay.FindAsync(id);
            if (barangay == null) return NotFound();

            _context.Barangay.Remove(barangay);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}