using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Storage.Models
{
    public class Evenement
    {
        public int Id { get; set; }
        
        [MaxLength(30)]
        public string Naam { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EindDateTime { get; set; }
        public DateTime LaatstGewijzigd { get; set; } = DateTime.Today;

        public Locatie Locatie { get; set; }
        public int LocatieId { get; set; }

    }
}
