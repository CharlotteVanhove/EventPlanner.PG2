using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Api.Contracts.Locatie
{
    public class LocatieResponseContract
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public double GpsLat { get; set; }
        public double GpsLong { get; set; }
    }

}

