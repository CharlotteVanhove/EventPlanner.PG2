using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Storage.ModelsMongoDB
{
    public class AuditTrail
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Onderwerp Onderwerp { get; set; }
        public Actie Actie { get; set; }
        public string OudeWaarde { get; set; }
        public string NieuweWaarde { get; set; }
    }
}
