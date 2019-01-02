using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<FavoriteAnimal> FavoriteAnimals { get; set; }
        public int RightToUserId { get; set; }
        public UserToAnimalShelter UserToAnimalShelter { get; set; }
        public RoleEnum Role { get; set; }
        
        [InverseProperty("User")]
        public List<RightsToUser> RightsToUser { get; set; }
    }
}
