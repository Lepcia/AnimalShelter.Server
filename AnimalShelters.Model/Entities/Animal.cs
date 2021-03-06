﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelters.Model.Entities
{
    public class Animal : IEntityBase
    {
        public Animal() {
            Photos = new List<Photo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public AnimalsToAnimalShelter AnimalsToAnimalShelter { get; set; }
        public AnimalAgeAccuracyEnum AgeAccuracy { get; set; }
        public string Avatar { get; set; }
        public AnimalShelter AnimalShelter { get; set; }

        public DateTime DateOfBirth { get; set; }
        private string ageString { get; set; }
        public string AgeString {
            get { return ageString; }
            set { ageString = "" + Age + " " + AgeAccuracy.ToString(); }
        }

        public AnimalSpeciesEnum Species { get; set; }
        public string Breed { get; set; }
        public AnimalSexEnum Sex { get; set; }
        public AnimalSizeEnum Size { get; set; }
        public string Description { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public DateTime InShelterFrom { get; set; }
    }
}
