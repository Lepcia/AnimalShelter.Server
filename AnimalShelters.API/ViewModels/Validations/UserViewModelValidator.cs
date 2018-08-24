using FluentValidation;

namespace AnimalShelters.API.ViewModels.Validations
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().WithMessage("Firstname cannot be empty!");
            RuleFor(user => user.LastName).NotEmpty().WithMessage("Lastname cannot be empty!");
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email cannot be mepty!");
            RuleFor(user => user.Email).EmailAddress().WithMessage("Wrong email address format!");
        }
    }
}
