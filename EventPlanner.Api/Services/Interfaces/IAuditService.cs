using EventPlanner.Api.Contracts.AuditTrail;
using EventPlanner.Api.Contracts.Event;
using EventPlanner.Storage.ModelsMongoDB;

namespace EventPlanner.Api.Services.Interfaces
{
    public interface IAuditService
    {
        Task LogCreate(Onderwerp onderwerp, object waarde);
        Task LogRead(Onderwerp onderwerp, object waarde);
        Task LogUpdate(Onderwerp onderwerp, object oudeWaarde, object nieuweWaarde);
        Task LogDelete(Onderwerp onderwerp, object waarde);
        Task<List<AuditTrailResponseContraact>> GetAllAuditTrailsAsync(string onderwerp = null , string actie = null);


    }
}
