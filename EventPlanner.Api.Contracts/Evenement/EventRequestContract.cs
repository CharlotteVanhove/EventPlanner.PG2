using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Api.Contracts.Event
{
    public class EventRequestContract
    {
        [MaxLength(30)]
        public string Naam { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EindDateTime { get; set; }
        public int LocatieId { get; set; }
    }
}
