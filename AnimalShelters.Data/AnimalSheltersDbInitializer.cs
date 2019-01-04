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
                    DateOfBirth = new DateTime(2016,11,10),
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
                    DateOfBirth = new DateTime(2018,05,12),
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
                    DateOfBirth = new DateTime(2016,03,14),
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

                    },
                    Role = RoleEnum.Admin
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
                    },
                    Role = RoleEnum.CommonUser
                };
                User user_02 = new User
                {
                    FirstName = "Agata",
                    LastName = "Socha",
                    FavoriteAnimals = {
                        new FavoriteAnimal { AnimalId = 1, UserId = 3 }
                    },
                    Role = RoleEnum.ShelterUser,
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
                    Role = RoleEnum.ShelterUser,
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
                Module module_02 = new Module { Id = 2, Symbol = "FAVORITE", Name = "Favorite animals", Icon = "ic_favorite_black_18dp", Order = 4 };
                Module module_03 = new Module { Id = 3, Symbol = "SHELTERS", Name = "Animal shelters", Icon = "ic_assignment_black_18dp", Order = 6 };
                Module module_04 = new Module { Id = 4, Symbol = "ANIMALS", Name = "Animals", Icon = "ic_pets_black_18dp", Order = 5 };
                Module module_05 = new Module { Id = 5, Symbol = "SEARCH", Name = "Search", Icon = "baseline_search_black_18dp", Order = 3 };
                Module module_06 = new Module { Id = 6, Symbol = "NEWS", Name = "News board", Icon = "ic_list_black_18dp", Order = 2 };
                Module module_07 = new Module { Id = 7, Symbol = "SETTINGS", Name = "Settings", Icon = "baseline_settings_black_18dp", Order = 8 };
                Module module_08 = new Module { Id = 8, Symbol = "PHOTOS", Name = "Photos", Icon = "baseline_local_see_black_18dp", Order = 7 };

                context.Modules.Add(module_01);
                context.Modules.Add(module_02);
                context.Modules.Add(module_03);
                context.Modules.Add(module_04);
                context.Modules.Add(module_05);
                context.Modules.Add(module_06);
                context.Modules.Add(module_07);
                context.Modules.Add(module_08);

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
                Rights right_10 = new Rights { Symbol = "DEFAULT", Name = "News board", IdModule = 6 };
                Rights right_11 = new Rights { Symbol = "DEFAULT", Name = "Settings", IdModule = 7 };
                Rights right_12 = new Rights { Symbol = "DEFAULT", Name = "Photos", IdModule = 8 };

                context.Rights.Add(right_01);
                context.Rights.Add(right_02);
                context.Rights.Add(right_03);
                context.Rights.Add(right_04);
                context.Rights.Add(right_05);
                context.Rights.Add(right_06);
                context.Rights.Add(right_07);
                context.Rights.Add(right_08);
                context.Rights.Add(right_09);
                context.Rights.Add(right_10);
                context.Rights.Add(right_11);
                context.Rights.Add(right_12);

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
                RightsToUser rightToUser_20 = new RightsToUser { IdRight = 10, IdUser = 1 };
                RightsToUser rightToUser_21 = new RightsToUser { IdRight = 10, IdUser = 2 };
                RightsToUser rightToUser_22 = new RightsToUser { IdRight = 10, IdUser = 3 };
                RightsToUser rightToUser_23 = new RightsToUser { IdRight = 10, IdUser = 4 };
                RightsToUser rightToUser_24 = new RightsToUser { IdRight = 11, IdUser = 1 };
                RightsToUser rightToUser_25 = new RightsToUser { IdRight = 11, IdUser = 2 };
                RightsToUser rightToUser_26 = new RightsToUser { IdRight = 11, IdUser = 3 };
                RightsToUser rightToUser_27 = new RightsToUser { IdRight = 11, IdUser = 4 };
                RightsToUser rightToUser_28 = new RightsToUser { IdRight = 12, IdUser = 1 };
                RightsToUser rightToUser_29 = new RightsToUser { IdRight = 12, IdUser = 3};
                RightsToUser rightToUser_30 = new RightsToUser { IdRight = 12, IdUser = 4 };

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
                context.RightsToUsers.Add(rightToUser_20);
                context.RightsToUsers.Add(rightToUser_21);
                context.RightsToUsers.Add(rightToUser_22);
                context.RightsToUsers.Add(rightToUser_23);
                context.RightsToUsers.Add(rightToUser_24);
                context.RightsToUsers.Add(rightToUser_25);
                context.RightsToUsers.Add(rightToUser_26);
                context.RightsToUsers.Add(rightToUser_27);
                context.RightsToUsers.Add(rightToUser_28);
                context.RightsToUsers.Add(rightToUser_29);
                context.RightsToUsers.Add(rightToUser_30);

                context.SaveChanges();
            }
        }
    }
}
