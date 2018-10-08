using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Model.Entities
{
    public class Rights : IEntityBase
    {
        public Rights() { }

        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
    }
}
