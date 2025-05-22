using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Storage
{
    public class EventPlannerDbContext : DbContext
    {
        public EventPlannerDbContext(DbContextOptions<EventPlannerDbContext> options)
         : base(options)
        {
        }

        public DbSet<Models.Evenement> Evenementen { get; set; }
        public DbSet<Models.Locatie> Locaties { get; set; }
        public DbSet<Models.Taak> Taken { get; set; }
    }
}
