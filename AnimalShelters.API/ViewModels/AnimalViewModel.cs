using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AnimalShelters.API.ViewModels.Validations;

namespace AnimalShelters.API.ViewModels
{
    public class AnimalViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string AgeAccuracy { get; set; }

        private string ageString { get; set; }
        public string AgeString
        {
            get { return ageString; }
            set { ageString = "" + Age + " " + AgeAccuracy.ToString(); }
        }

        public string Species { get; set; }
        public string Breed { get; set; }
        public string Sex { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public int[] Photos { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new AnimalViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
