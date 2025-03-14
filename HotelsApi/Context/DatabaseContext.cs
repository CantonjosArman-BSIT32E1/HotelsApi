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
                entity.HasMany(h => h.Transactions).WithOne(t => t.Hotel).HasForeignKey(t => t.HotelId);
                entity.HasIndex(e => e.BarangayId, "IX_Hotels_BarangayId");
                entity.HasOne(d => d.Barangay).WithMany(p => p.Hotels).HasForeignKey(d => d.BarangayId);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryId).HasName("PRIMARY");
                entity.Property(e => e.CountryCode).IsRequired().HasMaxLength(10);
                entity.Property(e => e.CountryName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.StateId).HasName("PRIMARY");
                entity.HasIndex(e => e.CountryId, "IX_State_CountryId");
                entity.HasOne(d => d.Country).WithMany(p => p.State).HasForeignKey(d => d.CountryId);
                entity.Property(e => e.StateCode).HasMaxLength(10);
                entity.Property(e => e.StateName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CityId).HasName("PRIMARY");
                entity.HasIndex(e => e.StateId, "IX_City_StateId");
                entity.HasOne(d => d.State).WithMany(p => p.City).HasForeignKey(d => d.StateId);
                entity.Property(e => e.CityCode).HasMaxLength(10);
                entity.Property(e => e.CityName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Barangay>(entity =>
            {
                entity.HasKey(e => e.BarangayId).HasName("PRIMARY");
                entity.HasIndex(e => e.CityId, "IX_Barangay_CityId");
                entity.HasOne(d => d.City).WithMany(p => p.Barangay).HasForeignKey(d => d.CityId);
                entity.Property(e => e.BarangayName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PostalCode).IsRequired().HasMaxLength(10);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId).HasName("PRIMARY");
                entity.HasIndex(e => e.HotelId, "IX_Transactions_HotelId");
                entity.HasOne(t => t.Hotel).WithMany(h => h.Transactions).HasForeignKey(t => t.HotelId);
                entity.Property(e => e.HotelName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.HotelCode).IsRequired().HasMaxLength(50);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(150);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.EmailAddress).IsRequired().HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
