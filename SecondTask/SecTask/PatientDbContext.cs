using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SecTask.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SecTask
{
    public class PatientDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();

            modelBuilder.Entity<BMIDataAge>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("GetBMIStatisticsByAge");
                entity.Property(e => e.AgeRange).HasColumnName("AgeRange");
                entity.Property(e => e.BMICharacteristic).HasColumnName("BMICharacteristic");
                entity.Property(e => e.Percentage).HasColumnName("Percentage");
            });

        }
        public DbSet<Patient> patient { get; set; }

        public DbSet<BMIDataAge> BMIDataAge { get; set; }

        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options)
        {

        }

    }
}

