using CVManager.DAL.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CVManager.DAL
{
    public class CVManagerDbContext : DbContext
    {
        public CVManagerDbContext(DbContextOptions<CVManagerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cv> Cvs { get; set; }
        public DbSet<PersonalInformation> PersonalInformation { get; set; }
        public DbSet<ExperienceInformation> ExperienceInformation { get; set; }
    }
}
