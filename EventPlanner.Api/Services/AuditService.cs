using EventPlanner.Api.Contracts.AuditTrail;
using EventPlanner.Api.Services.Interfaces;
using EventPlanner.Storage;
using EventPlanner.Storage.ModelsMongoDB;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EventPlanner.Api.Services
{
    public class AuditService : IAuditService
    {
        private readonly AuditDBContext _context;

        public AuditService(AuditDBContext context)
        {
            _context = context;
        }

        public async Task<List<AuditTrailResponseContraact>> GetAllAuditTrailsAsync(string onderwerp = null, string actie = null)
        {
            var query = _context.AuditTrails.AsQueryable();

            if (!string.IsNullOrEmpty(onderwerp))
            {
                if (Enum.TryParse<Onderwerp>(onderwerp, out var onderwerpEnum))
                {
                    query = query.Where(a => a.Onderwerp == onderwerpEnum);
                }
            }

            if (!string.IsNullOrEmpty(actie))
            {
                if (Enum.TryParse<Actie>(actie, out var actieEnum))
                {
                    query = query.Where(a => a.Actie == actieEnum);
                }
            }

            var result = await query
                .Select(a => new AuditTrailResponseContraact
                {
                    Id = a.Id,
                    CreatedAt = a.CreatedAt,
                    Onderwerp = a.Onderwerp.ToString(),
                    Actie = a.Actie.ToString(),
                    OudeWaarde = a.OudeWaarde,
                    NieuweWaarde = a.NieuweWaarde,
                })
                .ToListAsync();

            return result;
        }

        public async Task LogCreate(Onderwerp onderwerp, object waarde)
        {
            var auditTrail = new AuditTrail
            {
                Onderwerp = onderwerp,
                Actie = Actie.Create,
                NieuweWaarde = JsonSerializer.Serialize(waarde),
            };

            _context.AuditTrails.Add(auditTrail);

            await _context.SaveChangesAsync();
        }

        public async Task LogDelete(Onderwerp onderwerp, object waarde)
        {
            var auditTrail = new AuditTrail
            {
                Onderwerp = onderwerp,
                Actie = Actie.Delete,
                NieuweWaarde = JsonSerializer.Serialize(waarde),
            };

            _context.AuditTrails.Add(auditTrail);

            await _context.SaveChangesAsync();
        }

        public async Task LogRead(Onderwerp onderwerp, object waarde)
        {
            var auditTrail = new AuditTrail
            {
                Onderwerp = onderwerp,
                Actie = Actie.Read,
                NieuweWaarde = JsonSerializer.Serialize(waarde),
            };

            _context.AuditTrails.Add(auditTrail);

            await _context.SaveChangesAsync();
        }

        public async Task LogUpdate(Onderwerp onderwerp, object oudeWaarde, object nieuweWaarde)
        {
            var auditTrail = new AuditTrail
            {
                Onderwerp = onderwerp,
                Actie = Actie.Update,
                OudeWaarde = JsonSerializer.Serialize(oudeWaarde),
                NieuweWaarde = JsonSerializer.Serialize(nieuweWaarde),
            };

            _context.AuditTrails.Add(auditTrail);

            await _context.SaveChangesAsync();
        }
    }
}
