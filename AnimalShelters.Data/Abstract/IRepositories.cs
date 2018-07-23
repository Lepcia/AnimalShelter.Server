using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Data.Abstract
{
    public interface IAnimalShelterRepository : IEntityBaseRepository<AnimalShelter> { }

    public interface IAnimalRepository : IEntityBaseRepository<Animal> { }

    public interface IUserRepository : IEntityBaseRepository<User> { }

    public interface IPhotoRepository : IEntityBaseRepository<Photo> { }
}
