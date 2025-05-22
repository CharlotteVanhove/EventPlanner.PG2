using EventPlanner.Api.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Api.Contracts.Taak
{
    public class TaakRequestContract
    {
        [MaxLength(150)]
        public string Naam { get; set; }
        [MaxLength(2000)]
        public string Beschrijving { get; set; }

        public DateTime DeadlineTime { get; set; }
        public Belangrijkheid Belangrijkheid { get; set; }
        public Status Status { get; set; }

        public int EvenementId { get; set; }

    }
}
