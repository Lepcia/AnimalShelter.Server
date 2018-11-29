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
        public DbSet<Module> Modules { get; set; }
        public DbSet<Rights> Rights { get; set; }
        public DbSet<RightsToUser> RightsToUsers { get; set; }
        public DbSet<UserToAnimalShelter> UserToAnimalShelters { get; set; }
        public DbSet<AnimalsToAnimalShelter> AnimalsToAnimalShelters { get; set; }

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

            modelBuilder.Entity<Module>()
                .Property(m => m.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Module>()
                .Property(m => m.Symbol)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Module>()
                .Property(m => m.Order)
                .IsRequired();
            
            modelBuilder.Entity<Rights>()
                .Property(r => r.Name)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Rights>()
                .Property(r => r.Symbol)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Rights>()
                .Property(r => r.IdModule)
                .IsRequired();

            modelBuilder.Entity<RightsToUser>()
                .Property(r => r.IdRight)
                .IsRequired();

            modelBuilder.Entity<RightsToUser>()
                .Property(r => r.IdUser)
                .IsRequired();


            modelBuilder.Entity<RightsToUser>()
                .HasOne(r => r.Right)
                .WithMany(r => r.RightsToUser)
                .HasForeignKey(r => r.IdRight);

            modelBuilder.Entity<RightsToUser>()
                .HasOne(r => r.User)
                .WithMany(u => u.RightsToUser)
                .HasForeignKey(u => u.IdUser);

            modelBuilder.Entity<Rights>()
                .HasOne(r => r.Module)
                .WithMany(m => m.Rights)
                .HasForeignKey(m => m.IdModule);

            modelBuilder.Entity<Rights>()
                .ToTable("Rights");

            modelBuilder.Entity<RightsToUser>()
                .ToTable("RightsToUser");

            modelBuilder.Entity<Module>()
                .ToTable("Module");

            modelBuilder.Entity<UserToAnimalShelter>()
                .ToTable("UserToAnimalShelter");

            modelBuilder.Entity<AnimalsToAnimalShelter>()
                .ToTable("AnimalsToAnimalShelter");
        }

    }
}
