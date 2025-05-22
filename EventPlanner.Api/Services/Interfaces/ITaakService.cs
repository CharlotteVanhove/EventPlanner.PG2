using EventPlanner.Api.Contracts.Taak;

namespace EventPlanner.Api.Services.Interfaces
{
    public interface ITaakService
    {        
        Task<TaakResponseContract> CreateTaakAsync(TaakRequestContract request);
        Task<List<TaakResponseContract>> GetAllTakenAsync();
        Task UpdateAsync(int id, TaakRequestContract contract);
        Task DeleteTaakAsync(int id);

    }
}
