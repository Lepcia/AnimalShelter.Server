using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Model.Entities
{
    public class UserToAnimalShelter : IEntityBase
    {
        public UserToAnimalShelter()
        { }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int AnimalShelterId { get; set; }

        public User User { get; set; }
        public AnimalShelter AnimalShelter { get; set; }
    }
}
