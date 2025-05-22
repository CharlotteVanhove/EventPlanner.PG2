using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Api.Contracts.Locatie
{
    public class LocatieRequestContract
    {
        [MaxLength(50)]
        public string Naam { get; set; }
        [MaxLength(2000)]
        public string Beschrijving { get; set; }
        public double GpsLat { get; set; }
        public double GpsLong { get; set; }
    }
}

