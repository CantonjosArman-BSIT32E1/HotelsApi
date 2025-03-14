using HotelsApi.Services;
using HotelsApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController : ControllerBase
    {
        private readonly IStateService stateService;

        public StateController(IStateService stateService)
        {
            this.stateService = stateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStates()
        {
            var states = await stateService.GetAllStates();
            return Ok(states);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStateById([FromRoute] int id)
        {
            var state = await stateService.GetStateById(id);
            return Ok(state);
        }

        [HttpPost]
        public async Task<IActionResult> CreateState([FromBody] CreateStateModel state)
        {
            var createdState = await stateService.CreateState(state);
            return Ok(createdState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateState([FromRoute] int id, [FromBody] UpdateStateModel state)
        {
            var updatedState = await stateService.UpdateState(state, id);
            return Ok(updatedState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState([FromRoute] int id)
        {
            var result = await stateService.DeleteState(id);
            if (result)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
