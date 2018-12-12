using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels
{
    public class AnimalDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string AgeAccuracy { get; set; }
        public string AgeString { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string Sex { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public ICollection<PhotoViewModel> Photos { get; set; }
        public DateTime InShelterFrom { get; set; }
        public AnimalShelterViewModel AnimalShelter { get; set; }
        public bool IsFavorite { get; set; }

        public string[] AgesAccuracy { get; set; }
        public string[] Sexes { get; set; }
        public string[] Sizes { get; set; }
    }
}
