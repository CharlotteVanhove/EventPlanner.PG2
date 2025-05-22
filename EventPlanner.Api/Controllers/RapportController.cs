using EventPlanner.Api.Contracts.Rapporten;
using EventPlanner.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Api.Controllers
{
    [Route("api/rapporten")]
    [ApiController]
    public class RapportController : ControllerBase
    {
        private readonly IRapportService _rapportService;

        public RapportController(IRapportService rapportService)
        {
            _rapportService = rapportService;
        }

        [HttpGet("overzicht")]
        public async Task<ActionResult<List<OverzichtRapportResponseContract>>> GetAllRapportenAsync()
        {
            var result = await _rapportService.GetAllRapportenAsync();
            return Ok(result);
        }
    }
}
