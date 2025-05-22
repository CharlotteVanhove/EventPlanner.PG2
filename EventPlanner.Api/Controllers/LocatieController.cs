using EventPlanner.Api.Contracts.Locatie;
using EventPlanner.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Api.Controllers
{
    [Route("api/locaties")]
    [ApiController]
    public class LocatieController(ILocatieService locatieService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocatieResponseContract>>> GetAllLocaties()
        {
            var locaties = await locatieService.GetAllLocatiesAsync();
            return Ok(locaties);
        }

        [HttpPost]
        public async Task<ActionResult<LocatieResponseContract>> CreateLocatie([FromBody] LocatieRequestContract request)
        {
            var locatie = await locatieService.CreateLocatieAsync(request);
            return CreatedAtAction(nameof(GetAllLocaties), new { id = locatie.Id }, locatie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocatie(int id, [FromBody] LocatieRequestContract request)
        {
            try
            {
                //todo nakijken
                await locatieService.UpdateLocatieAsync(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocatie(int id)
        {
            try
            {
                await locatieService.DeleteLocatieAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
