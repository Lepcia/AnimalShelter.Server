using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Data.Repositories
{
    public class RightsRepository : EntityBaseRepository<Rights>, IRightsRepository
    {
        public RightsRepository(AnimalShelterContext context) : base(context)
        { }
    }
}
