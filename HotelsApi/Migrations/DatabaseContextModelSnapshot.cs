﻿// <auto-generated />
using System;
using HotelsApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelsApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("HotelsApi.Entities.Barangay", b =>
                {
                    b.Property<int>("BarangayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("BarangayId"));

                    b.Property<string>("BarangayName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("BarangayId");

                    b.HasIndex("CityId");

                    b.ToTable("Barangay");
                });

            modelBuilder.Entity("HotelsApi.Entities.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CityId"));

                    b.Property<string>("CityCode")
                        .HasColumnType("longtext");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("CityId");

                    b.HasIndex("StateId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("HotelsApi.Entities.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CountryId"));

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CountryId");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("HotelsApi.Entities.Hotels", b =>
                {
                    b.Property<int>("HotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("HotelId"));

                    b.Property<int>("BarangayId")
                        .HasColumnType("int");

                    b.Property<string>("HotelCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("HotelDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("HotelName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("HotelId");

                    b.HasIndex("BarangayId");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("HotelsApi.Entities.State", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("StateId"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("StateCode")
                        .HasColumnType("longtext");

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("StateId");

                    b.HasIndex("CountryId");

                    b.ToTable("State");
                });

            modelBuilder.Entity("HotelsApi.Entities.Transactions", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("HotelCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("HotelName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("HotelsHotelId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("TransactionId");

                    b.HasIndex("HotelsHotelId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("HotelsApi.Entities.Barangay", b =>
                {
                    b.HasOne("HotelsApi.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("HotelsApi.Entities.City", b =>
                {
                    b.HasOne("HotelsApi.Entities.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("HotelsApi.Entities.Hotels", b =>
                {
                    b.HasOne("HotelsApi.Entities.Barangay", "Barangay")
                        .WithMany()
                        .HasForeignKey("BarangayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Barangay");
                });

            modelBuilder.Entity("HotelsApi.Entities.State", b =>
                {
                    b.HasOne("HotelsApi.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("HotelsApi.Entities.Transactions", b =>
                {
                    b.HasOne("HotelsApi.Entities.Hotels", "Hotels")
                        .WithMany()
                        .HasForeignKey("HotelsHotelId");

                    b.Navigation("Hotels");
                });
#pragma warning restore 612, 618
        }
    }
}
