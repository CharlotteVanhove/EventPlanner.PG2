using EventPlanner.Storage.ModelsMongoDB;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Storage
{
    public class AuditDBContext : DbContext
    {
        //todo : mongodb met kleine letters in het vervolg
        public DbSet<AuditTrail> AuditTrails { get; set; }

        public AuditDBContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AuditTrail>().ToCollection("AuditTrail");
        }
    }
}
