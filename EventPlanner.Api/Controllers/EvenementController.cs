using EventPlanner.Api.Contracts.Event;
using EventPlanner.Api.Services.Interfaces;
using EventPlanner.Storage.ModelsMongoDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Api.Controllers
{
    [Route("api/evenementen")]
    [ApiController]
    public class EvenementController(IEventService service) : ControllerBase
    {
        [HttpGet]
        public async Task <ActionResult<IEnumerable<EventResponseContract>>> GetAll()
        {
            return Ok(await service.GetAllEventsAsync());
        }

        [HttpPost]
        public async Task<ActionResult<EventResponseContract>> Create([FromBody] EventRequestContract request)
        {
            var result = await service.CreateEventAsync(request);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] EventRequestContract request)
        {
            await service.UpdateEventAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]int id)
        {
            try
            { 
                await service.DeleteEventAsync(id); 
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }
    }
}
