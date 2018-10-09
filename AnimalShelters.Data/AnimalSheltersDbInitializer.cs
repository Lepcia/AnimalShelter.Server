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
            if (!context.Modules.Any())
            {
                Module module_01 = new Module { Symbol = "ADMIN", Name = "Administration" };
                Module module_02 = new Module { Symbol = "FAVORITES", Name = "Favorites animals" };
                Module module_03 = new Module { Symbol = "SHELTERS", Name = "Animal shelters" };
                Module module_04 = new Module { Symbol = "ANIMALS", Name = "Animals" };

                context.Modules.Add(module_01);
                context.Modules.Add(module_02);
                context.Modules.Add(module_03);
                context.Modules.Add(module_04);

                context.SaveChanges();
            }
            if (!context.Rights.Any())
            {
                Rights right_01 = new Rights { Symbol = "DEFAULT", Name = "Administration", IdModule = 1 };
                Rights right_02 = new Rights { Symbol = "EDIT_USER", Name = "Edit user", IdModule = 1 };
                Rights right_03 = new Rights { Symbol = "ADD USER", Name = "Add user", IdModule = 1 };
                Rights right_04 = new Rights { Symbol = "USER_LIST", Name = "User list view", IdModule = 1 };
                Rights right_05 = new Rights { Symbol = "ADD_FAVORITE", Name = "Add favorite animal", IdModule = 2 };
                Rights right_06 = new Rights { Symbol = "DELETE_FAVORITE", Name = "Delete favorite animal", IdModule = 2 };

                context.Rights.Add(right_01);
                context.Rights.Add(right_02);
                context.Rights.Add(right_03);
                context.Rights.Add(right_04);
                context.Rights.Add(right_05);
                context.Rights.Add(right_06);

                context.SaveChanges();
            }
            if (!context.RightsToUsers.Any())
            {
                RightsToUser rightToUser_01 = new RightsToUser { IdRight = 1, IdUser = 1 };
                RightsToUser rightToUser_02 = new RightsToUser { IdRight = 2, IdUser = 1 };
                RightsToUser rightToUser_03 = new RightsToUser { IdRight = 3, IdUser = 1 };
                RightsToUser rightToUser_04 = new RightsToUser { IdRight = 4, IdUser = 1 };
                RightsToUser rightToUser_05 = new RightsToUser { IdRight = 5, IdUser = 1 };
                RightsToUser rightToUser_06 = new RightsToUser { IdRight = 5, IdUser = 2 };
                RightsToUser rightToUser_07 = new RightsToUser { IdRight = 6, IdUser = 2 };
                RightsToUser rightToUser_08 = new RightsToUser { IdRight = 5, IdUser = 3 };
                RightsToUser rightToUser_09 = new RightsToUser { IdRight = 6, IdUser = 3 };
                RightsToUser rightToUser_10 = new RightsToUser { IdRight = 4, IdUser = 3 };

                context.RightsToUsers.Add(rightToUser_01);
                context.RightsToUsers.Add(rightToUser_02);
                context.RightsToUsers.Add(rightToUser_03);
                context.RightsToUsers.Add(rightToUser_04);
                context.RightsToUsers.Add(rightToUser_05);
                context.RightsToUsers.Add(rightToUser_06);
                context.RightsToUsers.Add(rightToUser_07);
                context.RightsToUsers.Add(rightToUser_08);
                context.RightsToUsers.Add(rightToUser_09);
                context.RightsToUsers.Add(rightToUser_10);

                context.SaveChanges();
            }
        }
    }
}
