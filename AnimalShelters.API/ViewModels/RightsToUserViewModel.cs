using AnimalShelters.API.ViewModels.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels
{
    public class RightsToUserViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public int IdRight { get; set; }
        public int IdUser { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new RightsToUserViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
