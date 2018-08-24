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
            if (!context.Users.Any())
            {
                User user_01 = new User { FirstName = "Daria", LastName = "Lepa", DateOfBirth = new DateTime(1994, 1, 29), Email = "daria.lepa@gmail.com" };

                context.Users.Add(user_01);

                context.SaveChanges();
            }
        }
    }
}
