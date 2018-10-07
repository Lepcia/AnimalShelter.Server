using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels.Validations
{
    public class RightsToUserViewModelValidator : AbstractValidator<RightsToUserViewModel>
    {
        public RightsToUserViewModelValidator()
        {
            RuleFor(rightToUser => rightToUser.IdRight).NotEmpty().WithMessage("Id right cannot be empty!");
            RuleFor(rightToUser => rightToUser.IdUser).NotEmpty().WithMessage("Id user cannot be empty!");
        }
    }
}
