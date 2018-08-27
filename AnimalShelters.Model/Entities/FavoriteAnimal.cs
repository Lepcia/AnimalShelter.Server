using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Model.Entities
{
    public class FavoriteAnimal : IEntityBase
    {
        public FavoriteAnimal()
        { }

        public int Id { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
