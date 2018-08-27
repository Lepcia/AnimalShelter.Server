using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Data.Repositories
{
    public class FavoriteAnimalRepository : EntityBaseRepository<FavoriteAnimal>, IFavoriteAnimalRepository
    {
        public FavoriteAnimalRepository(AnimalShelterContext context) : base(context)
        { }
    }
}
