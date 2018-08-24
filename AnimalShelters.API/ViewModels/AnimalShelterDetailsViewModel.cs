using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels
{
    public class AnimalShelterDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string FullAdres { get; set; }
        public ICollection<AnimalViewModel> Animals { get; set; }
    }
}
