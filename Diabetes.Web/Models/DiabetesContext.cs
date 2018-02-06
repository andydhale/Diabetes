using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.Web.Models
{
    public class DiabetesContext : DbContext
    {
        public DbSet<Reading> Readings { get; set; }

        public DbSet<Food> Foods { get; set; }

        public DbSet<InsulinInjection> Injections { get; set; }

        public DiabetesContext(DbContextOptions<DiabetesContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reading>()
                .Property(p => p.Created).HasDefaultValue(DateTimeOffset.UtcNow);

            base.OnModelCreating(modelBuilder);
        }
    }
}
