using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public StateController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<State>> GetAllStates()
        {
            return await _context.State.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetStateById(int id)
        {
            var state = await _context.State.FindAsync(id);
            if (state == null) return NotFound();
            return state;
        }

        [HttpPost]
        public async Task<ActionResult<State>> CreateState([FromBody] State state)
        {
            _context.State.Add(state);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStateById), new { id = state.StateId }, state);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateState(int id, [FromBody] State state)
        {
            var existingState = await _context.State.FindAsync(id);
            if (existingState == null) return NotFound();

            existingState.StateCode = state.StateCode;
            existingState.StateName = state.StateName;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            var state = await _context.State.FindAsync(id);
            if (state == null) return NotFound();

            _context.State.Remove(state);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
