using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalShelters.Data
{
    public class AnimalSheltersDbInitializer
    {
        private static AnimalShelterContext context;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            context = (AnimalShelterContext)serviceProvider.GetService(typeof(AnimalShelterContext));

            InitializeAnimalShelters();
        }

        private static void InitializeAnimalShelters()
        {
            if (!context.Animals.Any())
            {
                Animal animal_01 = new Animal
                {
                    Name = "Burek",
                    Age = 2,
                    AgeAccuracy = AnimalAgeAccuracyEnum.Years,
                    Breed = "Owczarek",
                    Sex = AnimalSexEnum.Male,
                    Size = AnimalSizeEnum.Medium,
                    Species = AnimalSpeciesEnum.Dog,
                    Description = "Pies Burek."
                };
                Animal animal_02 = new Animal
                {
                    Name = "Jolka",
                    Age = 7,
                    AgeAccuracy = AnimalAgeAccuracyEnum.Months,
                    Breed = "Rogdoll",
                    Sex = AnimalSexEnum.Female,
                    Size = AnimalSizeEnum.Medium,
                    Species = AnimalSpeciesEnum.Cat,
                    Description = "Kotka Jolka."
                };

                context.Animals.Add(animal_01);
                context.Animals.Add(animal_02);

                context.SaveChanges();
            }
            if (!context.Users.Any())
            {
                User user_01 = new User
                {
                    FirstName = "Daria",
                    LastName = "Lepa",
                    DateOfBirth = new DateTime(1994, 1, 29),
                    Email = "daria.lepa@gmail.com",
                    FavoriteAnimals = {
                        new FavoriteAnimal { AnimalId = 1, UserId = 1 },
                        new FavoriteAnimal { AnimalId = 2, UserId = 1}
                    }
                };
                User user_02 = new User
                {
                    FirstName = "Agata",
                    LastName = "Socha",
                    FavoriteAnimals = {
                        new FavoriteAnimal { AnimalId = 1, UserId = 2 }
                    }
                };
                User user_03 = new User
                {
                    FirstName = "Damian",
                    LastName = "Keller",
                    FavoriteAnimals =
                    {
                        new FavoriteAnimal { AnimalId = 2, UserId = 3 }
                    }
                };

                context.Users.Add(user_01);
                context.Users.Add(user_02);
                context.Users.Add(user_03);

                context.SaveChanges();
            }
            if (!context.AnimalShelters.Any())
            {
                AnimalShelter animalShelter_01 = new AnimalShelter { Name = "Schronisko", City = "Gdańsk", Street = "Szeroka", Number = "22", PostalCode = "80-234" };

                context.AnimalShelters.Add(animalShelter_01);

                context.SaveChanges();
            }
        }
    }
}
