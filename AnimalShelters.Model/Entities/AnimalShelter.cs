using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnimalShelters.Model.Entities
{
    public class AnimalShelter : IEntityBase
    {
        public AnimalShelter() {
            Animals = new List<AnimalsToAnimalShelter>();
            UsersToAnimalShelter = new List<UserToAnimalShelter>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string BankAccountNumber { get; set; }

        private string fullAdres; 
        public string FullAdres {
            get {
                return fullAdres;
            }
            set {
                fullAdres = "" + Street + " " + Number + ", " + City + " " + PostalCode;
            }
        }

        public ICollection<AnimalsToAnimalShelter> Animals { get; set; }

        public ICollection<UserToAnimalShelter> UsersToAnimalShelter { get; set; }


    }
}
