using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels
{
    public class UserDetailsViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public ICollection<AnimalViewModel> FavoriteAnimals { get; set; }
        public AnimalShelterViewModel UserToAnimalShelter { get; set; }
        public Role Role { get; set; }
    }
}
