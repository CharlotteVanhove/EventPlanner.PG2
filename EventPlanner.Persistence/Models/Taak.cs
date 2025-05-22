using EventPlanner.Storage.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Storage.Models
{
    public class Taak
    {

        public int Id { get; set; }
       
        [MaxLength(150)]
        public string Naam { get; set; }
        [MaxLength(2000)]
        public string Beschrijving { get; set; }
        public DateTime LaatstGewijzigd { get; set; } = DateTime.Today;

        public DateTime DeadlineTime { get; set; }
        public Belangrijkheid Belangrijkheid { get; set; }
        public Status Status { get; set; }

        public Evenement Evenement { get; set; }
        public int EvenementId { get; set; }



    }
}
