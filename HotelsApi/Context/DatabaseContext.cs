using Microsoft.EntityFrameworkCore;
using HotelsApi.Entities;

namespace HotelsApi.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Barangay> Barangay { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotels>();
            modelBuilder.Entity<Country>();
            modelBuilder.Entity<State>();
            modelBuilder.Entity<City>();
            modelBuilder.Entity<Barangay>();
        }
    }
}

