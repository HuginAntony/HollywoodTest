using FluentValidation;
using Tournament.Core.Models;

namespace Tournament.Core.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .MaximumLength(256)
                .NotEmpty()
                .WithMessage("username is required.")
                .OverridePropertyName("username");

            RuleFor(x => x.Password)
                .MaximumLength(256)
                .NotEmpty()
                .WithMessage("password is required.")
                .OverridePropertyName("password");
        }
    }
}