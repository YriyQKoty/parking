using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Parking.Models;

namespace Parking.Db
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarsToInsert> CarsToInserts { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<ParkingDetail> ParkingDetails { get; set; }
        public virtual DbSet<ParkingHistory> ParkingHistories { get; set; }
        public virtual DbSet<Station> Stations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost,1433;User Id=sa;Password=P@ssword123;Database=Afonin_308_1;Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasIndex(e => e.Id, "Cars_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Number, "Cars_Number_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IsParked).HasDefaultValueSql("((0))");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.SeatsCount).HasDefaultValueSql("((4))");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Cars_Owners_Id_fk");
            });

            modelBuilder.Entity<CarsToInsert>(entity =>
            {
                entity.HasKey(e => e.RowId)
                    .HasName("PK__CarsToIn__FFEE74318FFAEF39");

                entity.ToTable("CarsToInsert");

                entity.HasIndex(e => e.Number, "UQ__CarsToIn__78A1A19DC5FE9EF6")
                    .IsUnique();

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(e => e.Id, "Cities_Id_uindex")
                    .IsUnique();
                
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasIndex(e => e.Id, "Owners_Id_uindex")
                    .IsUnique();
                
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Birthdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ParkingDetail>(entity =>
            {
                entity.HasIndex(e => e.Id, "ParkingDetails_Id_uindex")
                    .IsUnique();

                entity.Property(e => e.Bill)
                    .HasColumnType("smallmoney")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BookTime).HasColumnType("datetime");

                entity.Property(e => e.ExpireTime).HasColumnType("datetime");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.ParkingDetails)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("ParkingDetails_Cars_Id_fk");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.ParkingDetails)
                    .HasForeignKey(d => d.StationId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("ParkingDetails_Stations_fk");
            });

            modelBuilder.Entity<ParkingHistory>(entity =>
            {
                entity.ToTable("ParkingHistory");

                entity.HasIndex(e => e.Id, "ParkingHistory_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.OwnerId, "ParkingHistory_OwnerId_uindex")
                    .IsUnique();

                entity.Property(e => e.StartHistoryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Owner)
                    .WithOne(p => p.ParkingHistory)
                    .HasForeignKey<ParkingHistory>(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ParkingHistory_Owners_fk");
            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.HasIndex(e => e.Id, "Stations_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Stations_Name_uindex")
                    .IsUnique();

                entity.Property(e => e.CarsCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PlacesCount).HasDefaultValueSql("((50))");

                entity.Property(e => e.PricePerHour)
                    .HasColumnType("smallmoney")
                    .HasDefaultValueSql("((10))");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Stations)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Stations_Cities_Id_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
