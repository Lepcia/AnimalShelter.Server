﻿using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Data.Abstract
{
    public interface IAnimalShelterRepository : IEntityBaseRepository<AnimalShelter> { }

    public interface IAnimalRepository : IEntityBaseRepository<Animal> { }

    public interface IUserRepository : IEntityBaseRepository<User> { }

    public interface IPhotoRepository : IEntityBaseRepository<Photo> { }

    public interface IFavoriteAnimalRepository : IEntityBaseRepository<FavoriteAnimal> { }

    public interface IModuleRepository : IEntityBaseRepository<Module> { }

    public interface IRightsRepository : IEntityBaseRepository<Rights> { }

    public interface IRightsToUserRepository : IEntityBaseRepository<RightsToUser> { }

    public interface IUserToAnimalShelterRepository : IEntityBaseRepository<UserToAnimalShelter> { }

    public interface IAnimalsToAnimalShelterRepository : IEntityBaseRepository<AnimalsToAnimalShelter> { }

    public interface IRoleRepository : IEntityBaseRepository<Role> { }

    public interface IRightsToRoleRepository : IEntityBaseRepository<RightsToRole> { }
}
