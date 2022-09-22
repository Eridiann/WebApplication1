using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.NetworkInformation;

namespace WebApplication1.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Registros>()
                .Property(c => c.FechaRegistro)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Registros> Registros { get; set; }
    }
}

//add-migration [migrationName]
//update-database
