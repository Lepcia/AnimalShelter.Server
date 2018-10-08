using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Data.Repositories
{
    public class ModuleRepository : EntityBaseRepository<Module>, IModuleRepository
    {
        public ModuleRepository(AnimalShelterContext context) : base(context)
        { }
    }
}
