using Microsoft.EntityFrameworkCore;
using HotelsApi.Entities;

namespace HotelsApi.Context
{
    public partial class DatabaseContext : DbContext
    {

        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) 
            : base(options) 
        { 

        }

        public virtual DbSet<Hotels> Hotels { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Barangay> Barangay { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Hotels>(entity =>
            {
                entity.HasKey(e => e.HotelId).HasName("PRIMARY");
                entity.Property(e => e.HotelCode).IsRequired().HasMaxLength(10);
                entity.Property(e => e.HotelName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.HotelDescription).IsRequired(false);
                entity.HasMany(h => h.Transactions).WithOne(t => t.Hotel).HasForeignKey(t => t.HotelId);
                entity.HasIndex(e => e.BarangayId, "Hotels_BarangayId_FK");
                entity.HasOne(d => d.Barangay).WithMany(p => p.Hotels).HasForeignKey(d => d.BarangayId).IsRequired(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryId).HasName("PRIMARY");
                entity.Property(e => e.CountryCode).IsRequired().HasMaxLength(10);
                entity.Property(e => e.CountryName).IsRequired().HasMaxLength(100);
                entity.HasMany(h => h.State).WithOne(t => t.Country).HasForeignKey(t => t.CountryId);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.StateId).HasName("PRIMARY");
                entity.HasIndex(e => e.CountryId, "State_CountryId_FK");
                entity.HasOne(d => d.Country).WithMany(p => p.State).HasForeignKey(d => d.CountryId);
                entity.HasMany(h => h.City).WithOne(t => t.State).HasForeignKey(t => t.StateId);
                entity.Property(e => e.StateCode).HasMaxLength(10);
                entity.Property(e => e.StateName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CityId).HasName("PRIMARY");
                entity.HasIndex(e => e.StateId, "City_StateId_FK");
                entity.HasOne(d => d.State).WithMany(p => p.City).HasForeignKey(d => d.StateId);
                entity.HasMany(h => h.Barangay).WithOne(t => t.City).HasForeignKey(t => t.CityId);
                entity.Property(e => e.CityCode).HasMaxLength(10);
                entity.Property(e => e.CityName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Barangay>(entity =>
            {
                entity.HasKey(e => e.BarangayId).HasName("PRIMARY");
                entity.HasIndex(e => e.CityId, "Barangay_CityId_FK");
                entity.HasOne(d => d.City).WithMany(p => p.Barangay).HasForeignKey(d => d.CityId);
                entity.HasMany(h => h.Hotels).WithOne(t => t.Barangay).HasForeignKey(t => t.BarangayId);
                entity.Property(e => e.BarangayName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PostalCode).IsRequired().HasMaxLength(10);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId).HasName("PRIMARY");
                entity.HasIndex(e => e.HotelId, "Transactions_HotelId_FK");
                entity.HasOne(t => t.Hotel).WithMany(h => h.Transactions).HasForeignKey(t => t.HotelId);
                entity.Property(e => e.HotelName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.HotelCode).IsRequired().HasMaxLength(10);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(15);
                entity.Property(e => e.EmailAddress).IsRequired().HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
