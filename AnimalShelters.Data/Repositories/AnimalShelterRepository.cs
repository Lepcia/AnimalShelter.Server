using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Data.Repositories
{
    public class AnimalShelterRepository : EntityBaseRepository<AnimalShelter>, IAnimalShelterRepository
    {
        public AnimalShelterRepository(AnimalShelterContext context) : base(context)
        { }
    }
}
