using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Model.Entities
{
    public class AnimalsToAnimalShelter : IEntityBase
    {
        public AnimalsToAnimalShelter()
        { }

        public int Id { get; set; }
        public int AnimalId { get; set; }
        public int AnimalShelterId { get; set; }

        public Animal Animal { get; set; }
        public AnimalShelter AnimalShelter { get; set; }
    }
}
