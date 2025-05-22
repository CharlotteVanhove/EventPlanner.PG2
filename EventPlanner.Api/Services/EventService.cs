using EventPlanner.Api.Contracts.Event;
using EventPlanner.Api.Services.Interfaces;
using EventPlanner.Storage;
using EventPlanner.Storage.Models;
using EventPlanner.Storage.ModelsMongoDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EventPlanner.Api.Services
{
    public class EventService : IEventService
    {
        private readonly EventPlannerDbContext _dbContext;
        private readonly IAuditService _auditService;

        public EventService(EventPlannerDbContext dbContext, IConfiguration configuration, IAuditService auditService)
        {
            _dbContext = dbContext;
            _auditService = auditService;
        }
        public async Task<EventResponseContract> CreateEventAsync(EventRequestContract request)
        {
            var locatie = await _dbContext.Locaties.FindAsync(request.LocatieId);
            if (locatie is null)
                throw new Exception("Locatie bestaat niet.");

            var evenement = new Evenement()
            {
                Naam = request.Naam,
                StartDateTime = request.StartDateTime,
                EindDateTime = request.EindDateTime,
                LocatieId = request.LocatieId
            };

            _dbContext.Evenementen.Add(evenement);
            await _dbContext.SaveChangesAsync();

            await _auditService.LogCreate(Onderwerp.Evenement, evenement);

            return MapToContract(evenement);
        }

        public async Task DeleteEventAsync(int id)
        {
            var evenement = await _dbContext.Evenementen.FindAsync(id);

            if (evenement is null) return;
            _dbContext.Evenementen.Remove(evenement);
            await _dbContext.SaveChangesAsync();
            await _auditService.LogDelete(Onderwerp.Evenement, evenement);

        }

        public async Task<List<EventResponseContract>> GetAllEventsAsync()
        {
            var evenementen = await _dbContext.Evenementen.Select(efp => MapToContract(efp)).ToListAsync();
            await _auditService.LogRead(Onderwerp.Evenement, evenementen);

            return evenementen;
        }

        public async Task UpdateEventAsync(int id, EventRequestContract contract)
        {
            var entity = await _dbContext.Evenementen.FindAsync(id);

            if (entity is null)
                throw new Exception("Evenement bestaat niet.");
            await _auditService.LogUpdate(Onderwerp.Evenement, entity, contract);

            entity.Naam = contract.Naam;
            entity.StartDateTime = contract.StartDateTime;
            entity.EindDateTime = contract.EindDateTime;


            await _dbContext.SaveChangesAsync();
        }

        private static EventResponseContract MapToContract(Evenement evenement)
        {
            return new EventResponseContract()
            {
                Id = evenement.Id,
                Naam = evenement.Naam,
                StartDateTime = evenement.StartDateTime,
                EindDateTime = evenement.EindDateTime,
            };
        }
    }
}
