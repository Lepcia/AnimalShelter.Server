using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels.Validations
{
    public class ModuleViewModelValidator : AbstractValidator<ModuleViewModel>
    {
        public ModuleViewModelValidator()
        {
            RuleFor(module => module.Symbol).NotEmpty().WithMessage("Symbol cannot be empty!");
            RuleFor(module => module.Name).NotEmpty().WithMessage("Name cannot be empty!");
            RuleFor(module => module.Order).NotEmpty().WithMessage("Order cannot be empty!");
        }
    }
}
