using EventPlanner.Api.Contracts.AuditTrail;
using EventPlanner.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Api.Controllers
{
    [Route("api/audittrails")]
    [ApiController]
    public class AuditTrailController : ControllerBase
    {
        private readonly IAuditService _auditService;

        public AuditTrailController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditTrailResponseContraact>>> GetAll([FromQuery] string onderwerp = null, [FromQuery] string actie = null)
        {
            var auditTrails = await _auditService.GetAllAuditTrailsAsync(onderwerp, actie);

            return Ok(auditTrails);
        }
    }
}
