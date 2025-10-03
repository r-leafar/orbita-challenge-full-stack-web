using EdTech.Core.Entities;
using EdTech.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EdTech.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new NationalIdentifierConfiguration());
        }

        public DbSet<Student> Students { get; set; }

    }
}
