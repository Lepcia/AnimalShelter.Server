using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Data.Repositories
{
    public class RightsToUserRepository : EntityBaseRepository<RightsToUser>, IRightsToUserRepository
    {
        public RightsToUserRepository(AnimalShelterContext context) : base(context)
        { }
    }
}
