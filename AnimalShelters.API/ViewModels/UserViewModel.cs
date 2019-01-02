using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AnimalShelters.API.ViewModels.Validations;

namespace AnimalShelters.API.ViewModels
{
    public class UserViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public int[] FavoriteAnimals { get; set; }
        public int UserToAnimalShelter { get; set; }
        public string Role { get; set; }
        public string ShelterName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new UserViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
