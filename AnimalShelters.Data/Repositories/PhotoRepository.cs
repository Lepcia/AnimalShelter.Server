using AnimalShelters.Data.Abstract;
using AnimalShelters.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Data.Repositories
{
    public class PhotoRepository : EntityBaseRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(AnimalShelterContext context) : base(context)
        { }
    }
}
