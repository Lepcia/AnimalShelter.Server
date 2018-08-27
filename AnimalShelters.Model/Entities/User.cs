using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Model.Entities
{
    public class User : IEntityBase
    {
        public User() {
            FavoriteAnimals = new List<FavoriteAnimal>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public ICollection<FavoriteAnimal> FavoriteAnimals { get; set; }
    }
}
