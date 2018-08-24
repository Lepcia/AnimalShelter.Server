using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels.Validations
{
    public class AnimalShelterViewModelValidator : AbstractValidator<AnimalShelterViewModel>
    {
        public AnimalShelterViewModelValidator()
        {
            RuleFor(animalShelter => animalShelter.Name).NotEmpty().WithMessage("Animal shelter name cannot be empty!");
            RuleFor(animalShelter => animalShelter.City).NotEmpty().WithMessage("City cannot be empty!");
            RuleFor(animalShelter => animalShelter.Street).NotEmpty().WithMessage("Street cannot be empty!");
            RuleFor(animalShelter => animalShelter.Number).NotEmpty().WithMessage("Number cannot be empty!");
            RuleFor(animalShelter => animalShelter.PostalCode).NotEmpty().WithMessage("Postal code cannot be empty!");
        }
    }
}
