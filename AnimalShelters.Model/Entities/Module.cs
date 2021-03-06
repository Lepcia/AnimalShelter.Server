﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnimalShelters.Model.Entities
{
    public class Module : IEntityBase
    {
        public Module() { }

        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }

        public int RightId { get; set; }

        [InverseProperty("Module")]
        public List<Rights> Rights { get; set; }
    }
}
