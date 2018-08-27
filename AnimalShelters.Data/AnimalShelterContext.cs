using AnimalShelters.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalShelters.Data
{
    public class AnimalShelterContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalShelter> AnimalShelters { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FavoriteAnimal> FavoriteAnimals { get; set; }

        public AnimalShelterContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Animal>()
                .ToTable("Animal");

            modelBuilder.Entity<Animal>()
                .Property(a => a.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Animal>()
                .Property(a => a.Species)
                .IsRequired();

            modelBuilder.Entity<Animal>()
                .Property(a => a.Breed)
                .IsRequired();

            modelBuilder.Entity<Animal>()
                .Property(a => a.Sex)
                .IsRequired();

            modelBuilder.Entity<AnimalShelter>()
                .ToTable("AnimalShelter");

            modelBuilder.Entity<AnimalShelter>()
                .Property(s => s.Name)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<AnimalShelter>()
                .Property(s => s.City)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<AnimalShelter>()
                .Property(s => s.Street)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<AnimalShelter>()
                .Property(s => s.Number)
                .IsRequired();

            modelBuilder.Entity<Photo>()
                .ToTable("Photo");

            modelBuilder.Entity<Photo>()
                .Property(p => p.Content)
                .IsRequired();

            modelBuilder.Entity<User>()
                .ToTable("User");

            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<FavoriteAnimal>()
                .ToTable("FavoriteAnimal");

        }

    }
}
