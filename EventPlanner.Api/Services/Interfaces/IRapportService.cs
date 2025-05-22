using EventPlanner.Api.Contracts.Rapporten;

namespace EventPlanner.Api.Services.Interfaces
{
    public interface IRapportService
    {
        Task<List<OverzichtRapportResponseContract>> GetAllRapportenAsync();
    }
}
