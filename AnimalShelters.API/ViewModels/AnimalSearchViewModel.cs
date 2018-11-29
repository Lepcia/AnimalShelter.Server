using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels
{
    public class AnimalSearchViewModel
    {
        public string Name { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public string AgeAccuracy { get; set; }
        public string Specie { get; set; }
        public string Breed { get; set; }
        public string Sex { get; set; }
        public string Size { get; set; }
    }
}
