using EventPlanner.Api.Contracts.Locatie;

namespace EventPlanner.Api.Services.Interfaces
{
    public interface ILocatieService
    {
        Task<LocatieResponseContract> CreateLocatieAsync(LocatieRequestContract request);
        Task<List<LocatieResponseContract>> GetAllLocatiesAsync();
        Task UpdateLocatieAsync(int id, LocatieRequestContract contract);
        Task DeleteLocatieAsync(int id);
    }
}
