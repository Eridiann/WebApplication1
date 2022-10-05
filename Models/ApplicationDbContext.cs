using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.NetworkInformation;

namespace WebApplication1.Models
{
    public class ApplicationDbContext : IdentityDbContext
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

//DB Token
//string, user@example.com, DCEa2023!

//add-migration [migrationName]
//update-database
