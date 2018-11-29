using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Data.Repositories
{
    public class UserToAnimalShelterRepository : EntityBaseRepository<UserToAnimalShelter>, IUserToAnimalShelterRepository
    {
        public UserToAnimalShelterRepository(AnimalShelterContext context) : base(context)
        { }
    }
}
