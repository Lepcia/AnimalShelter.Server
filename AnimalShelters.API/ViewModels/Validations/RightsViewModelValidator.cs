using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels.Validations
{
    public class RightsViewModelValidator : AbstractValidator<RightsViewModel>
    {
        public RightsViewModelValidator()
        {
            RuleFor(right => right.Symbol).NotEmpty().WithMessage("Symbol cannot be empty!");
            RuleFor(right => right.Name).NotEmpty().WithMessage("Name cannot be empty!");
            RuleFor(right => right.IdModule).NotEmpty().WithMessage("Id module cannot be empty!");
        }
    }
}
