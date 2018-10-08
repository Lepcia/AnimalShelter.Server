﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Model.Entities
{
    public class RightsToUser : IEntityBase
    {
        public RightsToUser() { }

        public int Id { get; set; }
        public int IdRight { get; set; }
        public int IdUser { get; set; }
    }
}
