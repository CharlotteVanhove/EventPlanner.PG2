using EventPlanner.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Storage.Repository
{
    public interface IOverzichtRapportRepository
    {
        Task<List<OverzichtRapport>> GetOverzichtRapportenAsync();
    }
}
