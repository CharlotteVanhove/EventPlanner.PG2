using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Storage.Models
{
    public class OverzichtRapport
    {
        public int EvenementId { get; set; }
        public string EvenementNaam { get; set; }
        public int LocatieId { get; set; }
        public string LocatieNaam { get; set; }
        public double PercentageVoltooideTaken { get; set; }
        public int AantalMustTakenInTodo { get; set; }
        public DateTime LaatsteUpdateEvenementOfTaken { get; set; }

    }
}
