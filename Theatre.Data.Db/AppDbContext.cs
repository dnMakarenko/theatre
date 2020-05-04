using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Theatre.Data.Core.Models;

namespace Theatre.Data.Db
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Spectacle> Spectacles { get; set; }
        public virtual DbSet<SpectacleSession> Sessions { get; set; }
        public virtual DbSet<SpectacleSessionReservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
