using EventPlanner.Api.Contracts.Locatie;
using EventPlanner.Api.Services.Interfaces;
using EventPlanner.Storage;
using EventPlanner.Storage.Models;
using EventPlanner.Storage.ModelsMongoDB;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Api.Services
{
    public class LocatieService : ILocatieService
    {
        private readonly EventPlannerDbContext _dbContext;
        private readonly IAuditService _auditService;

        public LocatieService(EventPlannerDbContext dbContext, IAuditService auditService)
        {
            _dbContext = dbContext;
            _auditService = auditService;
        }
        public async Task<LocatieResponseContract> CreateLocatieAsync(LocatieRequestContract request)
        {
            //todo nakijken
            var locatie = await _dbContext.Locaties.FirstOrDefaultAsync(x => x.Naam == request.Naam);

            if (locatie is not null)
                throw new Exception("Locatie bestaat al.");

            locatie = new Locatie()
            {
                Naam = request.Naam,
                Beschrijving = request.Beschrijving,
                GpsLat = request.GpsLat,
                GpsLong = request.GpsLong
            };

            _dbContext.Locaties.Add(locatie);
            await _dbContext.SaveChangesAsync();
            await _auditService.LogCreate(Onderwerp.Locatie, locatie);

            return MapToContract(locatie);
        }

        public async Task DeleteLocatieAsync(int id)
        {
            var locatie= await _dbContext.Locaties.FindAsync(id);

            if (locatie is null) return;
            _dbContext.Locaties.Remove(locatie);
            await _dbContext.SaveChangesAsync();
            await _auditService.LogDelete(Onderwerp.Locatie, locatie);

        }

        public async Task<List<LocatieResponseContract>> GetAllLocatiesAsync()
        {
            var locaties = await _dbContext.Locaties.Select(efp => MapToContract(efp)).ToListAsync();
            await _auditService.LogRead(Onderwerp.Locatie, locaties);

            return locaties;
        }

        public async Task UpdateLocatieAsync(int id, LocatieRequestContract contract)
        {
            var entity = await _dbContext.Locaties.FindAsync(id);

            if (entity is null)
                throw new Exception("Locatie bestaat niet.");
            await _auditService.LogUpdate(Onderwerp.Locatie, entity, contract);

            entity.Naam = contract.Naam;
            entity.Beschrijving = contract.Beschrijving;
            entity.GpsLat = contract.GpsLat;
            entity.GpsLong = contract.GpsLong;

            await _dbContext.SaveChangesAsync();
        }

        private static LocatieResponseContract MapToContract(Locatie locatie)
        {
            return new LocatieResponseContract()
            {
                Id = locatie.Id,
                Naam = locatie.Naam,
                Beschrijving = locatie.Beschrijving,
                GpsLat = locatie.GpsLat,
                GpsLong = locatie.GpsLong
            };
        }
    }
}
