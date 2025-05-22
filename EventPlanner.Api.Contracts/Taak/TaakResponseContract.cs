using EventPlanner.Api.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Api.Contracts.Taak
{
    public class TaakResponseContract
    {
        public int Id { get; set; }

        public string Naam { get; set; }
        public string Beschrijving { get; set; }

        public DateTime DeadlineTime { get; set; }
        public Belangrijkheid Belangrijkheid { get; set; }
        public Status Status { get; set; }

        public TaakEvenementResponseContract Evenement { get; set; }

        public class TaakEvenementResponseContract
        {
            public int Id { get; set; }
            public string Naam { get; set; }
           
        }
    }
}
