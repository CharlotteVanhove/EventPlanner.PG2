using EventPlanner.Api.Contracts.Taak;
using EventPlanner.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Api.Controllers
{
    [Route("api/taken")]
    [ApiController]
    public class TaakController(ITaakService service) : ControllerBase
    {
        [HttpPost]

        public async Task<ActionResult<TaakResponseContract>> CreateTaakAsync([FromBody] TaakRequestContract request)
        {
            var result = await service.CreateTaakAsync(request);
            return Created();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTakenAsync()
        {
            var result = await service.GetAllTakenAsync();
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] TaakRequestContract contract)
        {
            await service.UpdateAsync(id, contract);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaakAsync(int id)
        {
            await service.DeleteTaakAsync(id);
            return NoContent();
        }

    }
}
