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
                    Description = "Pies Burek.",
                    InShelterFrom = new DateTime(2018, 09, 29),
                    AnimalsToAnimalShelter = new AnimalsToAnimalShelter { AnimalShelterId = 1, AnimalId = 1}
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
                    Description = "Kotka Jolka.",
                    InShelterFrom = new DateTime(2018, 10, 20),
                    AnimalsToAnimalShelter = new AnimalsToAnimalShelter { AnimalShelterId = 2, AnimalId = 2}
                };
                Animal animal_03 = new Animal
                {
                    Name = "Raban",
                    Age = 2,
                    AgeAccuracy = AnimalAgeAccuracyEnum.Years,
                    Breed = "American shorthair",
                    Sex = AnimalSexEnum.Male,
                    Size = AnimalSizeEnum.Medium,
                    Species = AnimalSpeciesEnum.Cat,
                    Description = "Kot Raban.",
                    InShelterFrom = new DateTime(2018, 06, 13),
                    AnimalsToAnimalShelter = new AnimalsToAnimalShelter { AnimalShelterId = 2, AnimalId = 3}
                };

                context.Animals.Add(animal_01);
                context.Animals.Add(animal_02);
                context.Animals.Add(animal_03);

                context.SaveChanges();
            }
            if (!context.Users.Any())
            {
                User admin = new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    DateOfBirth = new DateTime(1990, 2, 3),
                    Email = "admin@admin.pl",
                    FavoriteAnimals =
                    {
                        new FavoriteAnimal { AnimalId = 1, UserId = 1},

                    }
                };

                User user_01 = new User
                {
                    FirstName = "Daria",
                    LastName = "Lepa",
                    DateOfBirth = new DateTime(1994, 1, 29),
                    Email = "daria.lepa@gmail.com",
                    FavoriteAnimals = {
                        new FavoriteAnimal { AnimalId = 1, UserId = 2 },
                        new FavoriteAnimal { AnimalId = 2, UserId = 2}
                    }
                };
                User user_02 = new User
                {
                    FirstName = "Agata",
                    LastName = "Socha",
                    FavoriteAnimals = {
                        new FavoriteAnimal { AnimalId = 1, UserId = 3 }
                    },
                    UserToAnimalShelter = new UserToAnimalShelter { AnimalShelterId = 2, UserId = 3}
                };
                User user_03 = new User
                {
                    FirstName = "Damian",
                    LastName = "Keller",
                    FavoriteAnimals =
                    {
                        new FavoriteAnimal { AnimalId = 2, UserId = 4 }
                    },
                    UserToAnimalShelter =  new UserToAnimalShelter {AnimalShelterId = 1, UserId = 4}
                };

                context.Users.Add(admin);
                context.Users.Add(user_01);
                context.Users.Add(user_02);
                context.Users.Add(user_03);

                context.SaveChanges();
            }
            if (!context.AnimalShelters.Any())
            {
                AnimalShelter animalShelter_01 = new AnimalShelter {
                    Name = "Schronisko",
                    City = "Gdańsk",
                    Street = "Szeroka",
                    Number = "22",
                    PostalCode = "80-234",
                    Email ="schronisko@wp.pl",
                    Phone ="324 324 123"
                };
                AnimalShelter animalShelter_02 = new AnimalShelter {
                    Name = "Ciapkowo",
                    City = "Gdańsk",
                    Street = "Długa",
                    Number = "12",
                    PostalCode = "80-556",
                    Email = "ciapkowo@wp.pl",
                    Phone = "565 324 123"
                };

                context.AnimalShelters.Add(animalShelter_01);
                context.AnimalShelters.Add(animalShelter_02);

                context.SaveChanges();
            }
            if (!context.Modules.Any())
            {
                Module module_01 = new Module { Id = 1, Symbol = "ADMIN", Name = "Administration", Icon = "ic_https_black_18dp", Order = 1 };
                Module module_02 = new Module { Id = 2, Symbol = "FAVORITE", Name = "Favorite animals", Icon = "ic_favorite_black_18dp", Order = 3 };
                Module module_03 = new Module { Id = 3, Symbol = "SHELTERS", Name = "Animal shelters", Icon = "ic_assignment_black_18dp", Order = 5 };
                Module module_04 = new Module { Id = 4, Symbol = "ANIMALS", Name = "Animals", Icon = "ic_pets_black_18dp", Order = 4 };
                Module module_05 = new Module { Id = 5, Symbol = "SEARCH", Name = "Search", Icon = "baseline_search_black_18dp", Order = 2 };

                context.Modules.Add(module_01);
                context.Modules.Add(module_02);
                context.Modules.Add(module_03);
                context.Modules.Add(module_04);
                context.Modules.Add(module_05);

                context.SaveChanges();
            }
            if (!context.Rights.Any())
            {
                Rights right_01 = new Rights { Symbol = "DEFAULT", Name = "Administration", IdModule = 1 };
                Rights right_02 = new Rights { Symbol = "EDIT_USER", Name = "Edit user", IdModule = 1 };
                Rights right_03 = new Rights { Symbol = "ADD_USER", Name = "Add user", IdModule = 1 };
                Rights right_04 = new Rights { Symbol = "USER_LIST", Name = "User list view", IdModule = 1 };
                Rights right_05 = new Rights { Symbol = "ADD_FAVORITE", Name = "Add favorite animal", IdModule = 2 };
                Rights right_06 = new Rights { Symbol = "DELETE_FAVORITE", Name = "Delete favorite animal", IdModule = 2 };
                Rights right_07 = new Rights { Symbol = "DEFAULT", Name = "Search", IdModule = 5 };
                Rights right_08 = new Rights { Symbol = "ADD_ANIMAL", Name = "Add animal", IdModule = 4 };
                Rights right_09 = new Rights { Symbol = "ADD_SHELTER", Name = "Add shelter", IdModule = 3 };

                context.Rights.Add(right_01);
                context.Rights.Add(right_02);
                context.Rights.Add(right_03);
                context.Rights.Add(right_04);
                context.Rights.Add(right_05);
                context.Rights.Add(right_06);
                context.Rights.Add(right_07);
                context.Rights.Add(right_08);
                context.Rights.Add(right_09);

                context.SaveChanges();
            }
            if (!context.RightsToUsers.Any())
            {
                RightsToUser rightToUser_01 = new RightsToUser { IdRight = 1, IdUser = 1 };
                RightsToUser rightToUser_02 = new RightsToUser { IdRight = 2, IdUser = 1 };
                RightsToUser rightToUser_03 = new RightsToUser { IdRight = 3, IdUser = 1 };
                RightsToUser rightToUser_04 = new RightsToUser { IdRight = 4, IdUser = 1 };
                RightsToUser rightToUser_05 = new RightsToUser { IdRight = 5, IdUser = 1 };
                RightsToUser rightToUser_06 = new RightsToUser { IdRight = 1, IdUser = 2 };
                RightsToUser rightToUser_07 = new RightsToUser { IdRight = 2, IdUser = 2 };
                RightsToUser rightToUser_08 = new RightsToUser { IdRight = 3, IdUser = 2 };
                RightsToUser rightToUser_09 = new RightsToUser { IdRight = 4, IdUser = 2 };
                RightsToUser rightToUser_10 = new RightsToUser { IdRight = 5, IdUser = 2 };
                RightsToUser rightToUser_11 = new RightsToUser { IdRight = 5, IdUser = 3 };
                RightsToUser rightToUser_12 = new RightsToUser { IdRight = 6, IdUser = 3 };
                RightsToUser rightToUser_13 = new RightsToUser { IdRight = 5, IdUser = 4 };
                RightsToUser rightToUser_14 = new RightsToUser { IdRight = 6, IdUser = 4 };
                RightsToUser rightToUser_15 = new RightsToUser { IdRight = 4, IdUser = 4 };
                RightsToUser rightToUser_16 = new RightsToUser { IdRight = 6, IdUser = 1 };
                RightsToUser rightToUser_17 = new RightsToUser { IdRight = 7, IdUser = 1 };
                RightsToUser rightToUser_18 = new RightsToUser { IdRight = 8, IdUser = 1 };
                RightsToUser rightToUser_19 = new RightsToUser { IdRight = 9, IdUser = 1 };

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
                context.RightsToUsers.Add(rightToUser_11);
                context.RightsToUsers.Add(rightToUser_12);
                context.RightsToUsers.Add(rightToUser_13);
                context.RightsToUsers.Add(rightToUser_14);
                context.RightsToUsers.Add(rightToUser_15);
                context.RightsToUsers.Add(rightToUser_16);
                context.RightsToUsers.Add(rightToUser_17);
                context.RightsToUsers.Add(rightToUser_18);
                context.RightsToUsers.Add(rightToUser_19);

                context.SaveChanges();
            }
        }
    }
}
