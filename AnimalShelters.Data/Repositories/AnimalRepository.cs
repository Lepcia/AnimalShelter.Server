using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Data.Repositories
{
    public class AnimalRepository : EntityBaseRepository<Animal>, IAnimalRepository
    {
        public AnimalRepository(AnimalShelterContext context) : base(context)
        { }
    }
}
