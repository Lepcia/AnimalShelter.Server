using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AnimalShelters.API.ViewModels.Validations;

namespace AnimalShelters.API.ViewModels
{
    public class AnimalShelterViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public int[] Animals { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        private string fullAdres;
        public string FullAdres
        {
            get
            {
                return fullAdres;
            }
            set
            {
                fullAdres = "" + Street + " " + Number + ", " + City + " " + PostalCode;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new AnimalShelterViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
