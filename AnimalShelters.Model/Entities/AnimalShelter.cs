using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Model.Entities
{
    public class AnimalShelter : IEntityBase
    {
        public AnimalShelter() {
            Animals = new List<Animal>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }

        private string fullAdres; 
        public string FullAdres {
            get {
                return fullAdres;
            }
            set {
                fullAdres = "" + Street + " " + Number + ", " + City + " " + PostalCode;
            }
        }

        public ICollection<Animal> Animals { get; set; }


    }
}
