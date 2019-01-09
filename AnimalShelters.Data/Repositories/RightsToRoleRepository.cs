using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Data.Repositories
{
    public class RightsToRoleRepository : EntityBaseRepository<RightsToRole>, IRightsToRoleRepository
    {
        public RightsToRoleRepository(AnimalShelterContext context) : base(context)
        { }
    }
}
