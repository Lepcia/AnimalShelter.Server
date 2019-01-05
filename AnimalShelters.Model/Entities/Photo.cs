using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Model.Entities
{
    public class Photo: IEntityBase
    {
        public Photo() { }

        public int Id { get; set; }
        public int AnimalId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
    }
}
