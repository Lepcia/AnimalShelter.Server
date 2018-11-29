using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Data.Repositories
{
    public class AnimalsToAnimalShelterRepository : EntityBaseRepository<AnimalsToAnimalShelter>, IAnimalsToAnimalShelterRepository
    {
        public AnimalsToAnimalShelterRepository(AnimalShelterContext contex) : base(contex)
        { }
    }
}
