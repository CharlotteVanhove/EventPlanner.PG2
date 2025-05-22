using EventPlanner.Api.Contracts.Rapporten;
using EventPlanner.Api.Services.Interfaces;
using EventPlanner.Storage.Repository;

namespace EventPlanner.Api.Services
{
    public class RapportService : IRapportService
    {
        private readonly IOverzichtRapportRepository _repo;

        public RapportService(IOverzichtRapportRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<OverzichtRapportResponseContract>> GetAllRapportenAsync()
        {
            var result = await _repo.GetOverzichtRapportenAsync();

            return result.Select(r => new OverzichtRapportResponseContract
            {
                EvenementId = r.EvenementId,
                EvenementNaam = r.EvenementNaam,
                LocatieId = r.LocatieId,
                LocatieNaam = r.LocatieNaam,
                PercentageVoltooideTaken = r.PercentageVoltooideTaken,
                AantalMustTakenInTodo = r.AantalMustTakenInTodo,
                LaatsteUpdateEvenementOfTaken = r.LaatsteUpdateEvenementOfTaken
            }).ToList();
        }
    }
}
