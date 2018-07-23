using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Data.Repositories
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        public UserRepository(AnimalShelterContext context) : base(context)
        { }
    }
}
