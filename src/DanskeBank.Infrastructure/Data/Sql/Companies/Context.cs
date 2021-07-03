using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DanskeBank.Infrastructure.Data.Sql.Companies
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Entities.Company>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<Entities.Owner>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }

        public DbSet<Entities.Company> Companies { get; set; }
        public DbSet<Entities.Owner> Owners { get; set; }
    }
}
