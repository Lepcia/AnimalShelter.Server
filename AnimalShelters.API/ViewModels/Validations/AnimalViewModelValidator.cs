using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels.Validations
{
    public class AnimalViewModelValidator : AbstractValidator<AnimalViewModel>
    {
        public AnimalViewModelValidator()
        {
            RuleFor(animal => animal.Name).NotEmpty().WithMessage("Name cannot be empty!");
            RuleFor(animal => animal.Sex).NotEmpty().WithMessage("Sex cannot be empty!");
            RuleFor(animal => animal.Size).NotEmpty().WithMessage("Size cannot be empty!");
            RuleFor(animal => animal.Breed).NotEmpty().WithMessage("Breed cannot be empty!");
            RuleFor(animal => animal.AgeAccuracy).NotEmpty().WithMessage("Age accuracy cannot be empty!");
            RuleFor(animal => animal.Species).NotEmpty().WithMessage("Species cannot be empty!");
            RuleFor(animal => animal.Description).NotEmpty().WithMessage("Description cannot be empty!");
        }
    }
}
