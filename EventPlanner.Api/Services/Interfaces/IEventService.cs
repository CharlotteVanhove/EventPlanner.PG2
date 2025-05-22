
using EventPlanner.Api.Contracts.Event;

namespace EventPlanner.Api.Services.Interfaces
{
    public interface IEventService
    {
        Task<EventResponseContract> CreateEventAsync(EventRequestContract request);
        Task<List<EventResponseContract>> GetAllEventsAsync();
        Task UpdateEventAsync(int id, EventRequestContract contract);
        Task DeleteEventAsync(int id);
    }
}
