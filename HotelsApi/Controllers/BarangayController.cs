using HotelsApi.Services;
using HotelsApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarangayController : ControllerBase
    {
        private readonly IBarangayService _barangayService;

        public BarangayController(IBarangayService barangayService)
        {
            _barangayService = barangayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBarangays()
        {
            var listOfBarangays = await _barangayService.GetAllBarangays();
            return Ok(listOfBarangays);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBarangayById([FromRoute] int id)
        {
            var barangay = await _barangayService.GetBarangayById(id);
            return Ok(barangay);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBarangay([FromBody] CreateBarangayModel barangay)
        {
            var createBarangayResult = await _barangayService.CreateBarangay(barangay);
            return Ok(createBarangayResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBarangay([FromRoute] int id, [FromBody] UpdateBarangayModel barangay)
        {
            var updateBarangayResult = await _barangayService.UpdateBarangay(barangay, id);
            return Ok(updateBarangayResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarangay([FromRoute] int id)
        {
            var deleteBarangayResult = await _barangayService.DeleteBarangay(id);
            if (deleteBarangayResult)
                return Ok(deleteBarangayResult);
            else
                return BadRequest(deleteBarangayResult);
        }
    }
}
