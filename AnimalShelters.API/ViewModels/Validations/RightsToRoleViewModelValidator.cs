using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels.Validations
{
    public class RightsToRoleViewModelValidator : AbstractValidator<RightsToRoleViewModel>
    {
        public RightsToRoleViewModelValidator()
        {
            RuleFor(righsToRole => righsToRole.IdRight).NotEmpty().WithMessage("Id right cannot be empty");
            RuleFor(rightsToRole => rightsToRole.IdRole).NotEmpty().WithMessage("Id role cannot be empty!");
        }
    }
}
