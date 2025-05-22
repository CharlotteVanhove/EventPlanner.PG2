using EventPlanner.Api.Contracts.Enum;
using EventPlanner.Api.Contracts.Taak;
using EventPlanner.Api.Services.Interfaces;
using EventPlanner.Storage;
using EventPlanner.Storage.Models;
using EventPlanner.Storage.ModelsMongoDB;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Api.Services
{
    public class TaakService : ITaakService
    {
        private readonly EventPlannerDbContext _dbContext;
        private readonly IAuditService _auditService;

        public TaakService(EventPlannerDbContext dbContext, IAuditService auditService)
        {
            _dbContext = dbContext;
            _auditService = auditService;
        }

        public async Task<TaakResponseContract> CreateTaakAsync(TaakRequestContract request)
        {
            var evenement = await _dbContext.Evenementen.FindAsync(request.EvenementId);

            if (evenement is null)
                throw new Exception("Evenement bestaat niet.");

            var taak = new Taak()
            {
                Naam = request.Naam,
                Beschrijving = request.Beschrijving,
                DeadlineTime = request.DeadlineTime,
                Belangrijkheid = (Storage.Enums.Belangrijkheid)(int)request.Belangrijkheid,
                Status = (Storage.Enums.Status)(int)request.Status,
                Evenement = evenement
            };

            _dbContext.Taken.Add(taak);
            await _dbContext.SaveChangesAsync();
            await _auditService.LogCreate(Onderwerp.Taak, evenement);

            return MapToContract(taak);
        }

        public async Task DeleteTaakAsync(int id)
        {
            var taak = await _dbContext.Taken.FindAsync(id);

            if (taak is null) return;
            _dbContext.Taken.Remove(taak);
            await _dbContext.SaveChangesAsync();
            await _auditService.LogDelete(Onderwerp.Taak, taak);
        }

        public async Task UpdateAsync(int id, TaakRequestContract contract)
        {
            var entity = await _dbContext.Taken.FindAsync(id);
            if (entity is null)
                throw new Exception("Taak bestaat niet.");

            await _auditService.LogUpdate(Onderwerp.Locatie, entity, contract);

            entity.Naam = contract.Naam;
            entity.Beschrijving = contract.Beschrijving;
            entity.DeadlineTime = contract.DeadlineTime;

            _dbContext.Taken.Update(entity);
            await _dbContext.SaveChangesAsync();

        }
        public async Task<List<TaakResponseContract>> GetAllTakenAsync()
        {
            var entities = await _dbContext.Taken
                    .Include(t => t.Evenement)
                    .ToListAsync();
            await _auditService.LogRead(Onderwerp.Taak, entities);

            return entities.Select(efp => MapToContract(efp)).ToList();
        }
        private static TaakResponseContract MapToContract(Taak taak)
        {
            return new TaakResponseContract()
            {
                Id = taak.Id,
                Naam = taak.Naam,
                Beschrijving = taak.Beschrijving,
                DeadlineTime = taak.DeadlineTime,
                Belangrijkheid = (Belangrijkheid)taak.Belangrijkheid,
                Status = (Status)taak.Status,
                Evenement = new TaakResponseContract.TaakEvenementResponseContract()
                {
                    Id = taak.Evenement.Id,
                    Naam = taak.Evenement.Naam
                }
            };
        }


    }
}
