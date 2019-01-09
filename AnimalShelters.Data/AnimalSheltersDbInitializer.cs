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
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("Admin123", out passwordHash, out passwordSalt);
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
                    Role = new Role { Id = 1, Name = "Admin", Symbol = "ADMIN" },
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
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
                    Role = new Role { Id = 2, Name = "ShelterAdmin", Symbol = "SHELTER_ADMIN" },
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };
                User user_02 = new User
                {
                    FirstName = "Agata",
                    LastName = "Socha",
                    FavoriteAnimals = {
                        new FavoriteAnimal { AnimalId = 1, UserId = 3 }
                    },
                    Role = new Role { Id = 3, Name = "ShelterUser", Symbol = "SHELTER_USER" },
                UserToAnimalShelter = new UserToAnimalShelter { AnimalShelterId = 2, UserId = 3},
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };
                User user_03 = new User
                {
                    FirstName = "Damian",
                    LastName = "Keller",
                    FavoriteAnimals =
                    {
                        new FavoriteAnimal { AnimalId = 2, UserId = 4 }
                    },
                    Role = new Role { Id = 4, Name = "CommonUser", Symbol = "COMMON_USER" },
                    UserToAnimalShelter =  new UserToAnimalShelter {AnimalShelterId = 1, UserId = 4},
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
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
                Module module_05 = new Module { Id = 5, Symbol = "SEARCH", Name = "Search", Icon = "baseline_search_black_18dp", Order = 3 };
                Module module_06 = new Module { Id = 6, Symbol = "NEWS", Name = "News board", Icon = "ic_list_black_18dp", Order = 2 };
                Module module_07 = new Module { Id = 7, Symbol = "SETTINGS", Name = "Settings", Icon = "baseline_settings_black_18dp", Order = 8 };
                Module module_08 = new Module { Id = 8, Symbol = "PHOTOS", Name = "Photos", Icon = "baseline_local_see_black_18dp", Order = 7 };

                context.Modules.Add(module_01);
                context.Modules.Add(module_02);
                context.Modules.Add(module_05);
                context.Modules.Add(module_06);
                context.Modules.Add(module_07);
                context.Modules.Add(module_08);

                context.SaveChanges();
            }
            if (!context.Rights.Any())
            {
                Rights right_01 = new Rights { Id = 1, Symbol = "DEFAULT", Name = "Administration", IdModule = 1,  };
                Rights right_02 = new Rights { Id = 2, Symbol = "EDIT_USER", Name = "Edit user", IdModule = 1 };
                Rights right_03 = new Rights { Id = 3, Symbol = "ADD_USER", Name = "Add user", IdModule = 1 };
                Rights right_04 = new Rights { Id = 4, Symbol = "USER_LIST", Name = "User list view", IdModule = 1 };
                Rights right_05 = new Rights { Id = 5, Symbol = "ADD_FAVORITE", Name = "Add favorite animal", IdModule = 2 };
                Rights right_06 = new Rights { Id = 6, Symbol = "DELETE_FAVORITE", Name = "Delete favorite animal", IdModule = 2 };
                Rights right_07 = new Rights { Id = 7, Symbol = "DEFAULT", Name = "Search", IdModule = 5 };
                Rights right_10 = new Rights { Id = 10, Symbol = "DEFAULT", Name = "News board", IdModule = 6 };
                Rights right_11 = new Rights { Id = 11, Symbol = "DEFAULT", Name = "Settings", IdModule = 7 };
                Rights right_12 = new Rights { Id = 12, Symbol = "DEFAULT", Name = "Photos", IdModule = 8 };
                Rights right_13 = new Rights { Id = 13, Symbol = "DEFAULT", Name = "Favorite animals", IdModule = 2 };
                Rights right_14 = new Rights { Id = 14, Symbol = "EDIT_USER", Name = "Edit user", IdModule = 7 };
                Rights right_15 = new Rights { Id = 15, Symbol = "EDIT_SHELTER", Name = "Edit shelter", IdModule = 7 };
                Rights right_16 = new Rights { Id = 16, Symbol = "EDIT_SHELTER_USERS", Name = "Edit shelter users", IdModule = 7 };
                Rights right_17 = new Rights { Id = 17, Symbol = "EDIT_SHELTER_ANIMALS", Name = "Edit shelter animals", IdModule = 7 };

                context.Rights.Add(right_01);
                context.Rights.Add(right_02);
                context.Rights.Add(right_03);
                context.Rights.Add(right_04);
                context.Rights.Add(right_05);
                context.Rights.Add(right_06);
                context.Rights.Add(right_07);
                context.Rights.Add(right_10);
                context.Rights.Add(right_11);
                context.Rights.Add(right_12);

                context.SaveChanges();
            }
            if (!context.Roles.Any())
            {
                Role admin = new Role { Id = 1, Name = "Admin", Symbol = "ADMIN" };
                Role shelterAdmin = new Role { Id = 2, Name = "ShelterAdmin", Symbol = "SHELTER_ADMIN" };
                Role shelterUser = new Role { Id = 3, Name = "ShelterUser", Symbol = "SHELTER_USER" };
                Role commonUser = new Role { Id = 4, Name = "CommonUser", Symbol = "COMMON_USER" };

                context.Roles.Add(admin);
                context.Roles.Add(shelterAdmin);
                context.Roles.Add(shelterUser);
                context.Roles.Add(commonUser);

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
            if (!context.RightsToRoles.Any())
            {
                RightsToRole rightToAdmin_01 = new RightsToRole { IdRight = 1, IdRole = 1 };
                RightsToRole rightToAdmin_02 = new RightsToRole { IdRight = 2, IdRole = 1 };
                RightsToRole rightToAdmin_03 = new RightsToRole { IdRight = 3, IdRole = 1 };
                RightsToRole rightToAdmin_04 = new RightsToRole { IdRight = 4, IdRole = 1 };
                RightsToRole rightToAdmin_05 = new RightsToRole { IdRight = 5, IdRole = 1 };
                RightsToRole rightToAdmin_06 = new RightsToRole { IdRight = 6, IdRole = 1 };
                RightsToRole rightToAdmin_07 = new RightsToRole { IdRight = 7, IdRole = 1 };
                RightsToRole rightToAdmin_08 = new RightsToRole { IdRight = 8, IdRole = 1 };
                RightsToRole rightToAdmin_09 = new RightsToRole { IdRight = 9, IdRole = 1 };
                RightsToRole rightToAdmin_10 = new RightsToRole { IdRight = 10, IdRole = 1 };
                RightsToRole rightToAdmin_11 = new RightsToRole { IdRight = 12, IdRole = 1 };
                RightsToRole rightToAdmin_12 = new RightsToRole { IdRight = 13, IdRole = 1 };

                RightsToRole rightToShelterAdmin_01 = new RightsToRole { IdRight = 5, IdRole = 2 };
                RightsToRole rightToShelterAdmin_02 = new RightsToRole { IdRight = 6, IdRole = 2 };
                RightsToRole rightToShelterAdmin_03 = new RightsToRole { IdRight = 7, IdRole = 2 };
                RightsToRole rightToShelterAdmin_04 = new RightsToRole { IdRight = 8, IdRole = 2 };
                RightsToRole rightToShelterAdmin_05 = new RightsToRole { IdRight = 9, IdRole = 2 };
                RightsToRole rightToShelterAdmin_06 = new RightsToRole { IdRight = 10, IdRole = 2 };
                RightsToRole rightToShelterAdmin_07 = new RightsToRole { IdRight = 11, IdRole = 2 };
                RightsToRole rightToShelterAdmin_08 = new RightsToRole { IdRight = 12, IdRole = 2 };
                RightsToRole rightToShelterAdmin_09 = new RightsToRole { IdRight = 13, IdRole = 2 };
                RightsToRole rightToShelterAdmin_10 = new RightsToRole { IdRight = 14, IdRole = 2 };
                RightsToRole rightToShelterAdmin_11 = new RightsToRole { IdRight = 15, IdRole = 2 };
                RightsToRole rightToShelterAdmin_12 = new RightsToRole { IdRight = 16, IdRole = 2 };
                RightsToRole rightToShelterAdmin_13 = new RightsToRole { IdRight = 17, IdRole = 2 };

                RightsToRole rightToShelterUser_01 = new RightsToRole { IdRight = 5, IdRole = 3 };
                RightsToRole rightToShelterUser_02 = new RightsToRole { IdRight = 6, IdRole = 3 };
                RightsToRole rightToShelterUser_03 = new RightsToRole { IdRight = 7, IdRole = 3 };
                RightsToRole rightToShelterUser_04 = new RightsToRole { IdRight = 8, IdRole = 3 };
                RightsToRole rightToShelterUser_05 = new RightsToRole { IdRight = 9, IdRole = 3 };
                RightsToRole rightToShelterUser_06 = new RightsToRole { IdRight = 10, IdRole = 3 };
                RightsToRole rightToShelterUser_07 = new RightsToRole { IdRight = 11, IdRole = 3 };
                RightsToRole rightToShelterUser_08 = new RightsToRole { IdRight = 12, IdRole = 3 };
                RightsToRole rightToShelterUser_09 = new RightsToRole { IdRight = 13, IdRole = 3 };
                RightsToRole rightToShelterUser_10 = new RightsToRole { IdRight = 14, IdRole = 3 };
                RightsToRole rightToShelterUser_11 = new RightsToRole { IdRight = 17, IdRole = 3 };

                RightsToRole rightToUser_01 = new RightsToRole { IdRight = 5, IdRole = 4 };
                RightsToRole rightToUser_02 = new RightsToRole { IdRight = 6, IdRole = 4 };
                RightsToRole rightToUser_03 = new RightsToRole { IdRight = 7, IdRole = 4 };
                RightsToRole rightToUser_04 = new RightsToRole { IdRight = 8, IdRole = 4 };
                RightsToRole rightToUser_05 = new RightsToRole { IdRight = 9, IdRole = 4 };
                RightsToRole rightToUser_06 = new RightsToRole { IdRight = 10, IdRole = 4 };
                RightsToRole rightToUser_07 = new RightsToRole { IdRight = 11, IdRole = 4 };
                RightsToRole rightToUser_08 = new RightsToRole { IdRight = 13, IdRole = 4 };
                RightsToRole rightToUser_09 = new RightsToRole { IdRight = 14, IdRole = 4 };

                context.RightsToRoles.Add(rightToAdmin_01);
                context.RightsToRoles.Add(rightToAdmin_02);
                context.RightsToRoles.Add(rightToAdmin_03);
                context.RightsToRoles.Add(rightToAdmin_04);
                context.RightsToRoles.Add(rightToAdmin_05);
                context.RightsToRoles.Add(rightToAdmin_06);
                context.RightsToRoles.Add(rightToAdmin_07);
                context.RightsToRoles.Add(rightToAdmin_08);
                context.RightsToRoles.Add(rightToAdmin_09);
                context.RightsToRoles.Add(rightToAdmin_10);
                context.RightsToRoles.Add(rightToAdmin_11);
                context.RightsToRoles.Add(rightToAdmin_12);
                context.RightsToRoles.Add(rightToShelterAdmin_01);
                context.RightsToRoles.Add(rightToShelterAdmin_02);
                context.RightsToRoles.Add(rightToShelterAdmin_03);
                context.RightsToRoles.Add(rightToShelterAdmin_04);
                context.RightsToRoles.Add(rightToShelterAdmin_05);
                context.RightsToRoles.Add(rightToShelterAdmin_06);
                context.RightsToRoles.Add(rightToShelterAdmin_07);
                context.RightsToRoles.Add(rightToShelterAdmin_08);
                context.RightsToRoles.Add(rightToShelterAdmin_09);
                context.RightsToRoles.Add(rightToShelterAdmin_10);
                context.RightsToRoles.Add(rightToShelterAdmin_11);
                context.RightsToRoles.Add(rightToShelterAdmin_12);
                context.RightsToRoles.Add(rightToShelterAdmin_13);
                context.RightsToRoles.Add(rightToShelterUser_01);
                context.RightsToRoles.Add(rightToShelterUser_02);
                context.RightsToRoles.Add(rightToShelterUser_03);
                context.RightsToRoles.Add(rightToShelterUser_04);
                context.RightsToRoles.Add(rightToShelterUser_05);
                context.RightsToRoles.Add(rightToShelterUser_06);
                context.RightsToRoles.Add(rightToShelterUser_07);
                context.RightsToRoles.Add(rightToShelterUser_08);
                context.RightsToRoles.Add(rightToShelterUser_09);
                context.RightsToRoles.Add(rightToShelterUser_10);
                context.RightsToRoles.Add(rightToShelterUser_11);
                context.RightsToRoles.Add(rightToUser_01);
                context.RightsToRoles.Add(rightToUser_02);
                context.RightsToRoles.Add(rightToUser_03);
                context.RightsToRoles.Add(rightToUser_04);
                context.RightsToRoles.Add(rightToUser_05);
                context.RightsToRoles.Add(rightToUser_06);
                context.RightsToRoles.Add(rightToUser_07);
                context.RightsToRoles.Add(rightToUser_08);
                context.RightsToRoles.Add(rightToUser_09);

                context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
