﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
    }
}
