using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Api.Contracts.AuditTrail
{
    public class AuditTrailResponseContraact
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Onderwerp { get; set; }
        public string Actie { get; set; }
        public string OudeWaarde { get; set; }
        public string NieuweWaarde { get; set; }
    }
}
