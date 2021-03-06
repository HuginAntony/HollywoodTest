﻿using FluentValidation;
using Tournament.Core.Models;

namespace Tournament.Core.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username)
                .MaximumLength(256)
                .NotEmpty()
                .WithMessage("Username is required.")
                .OverridePropertyName("username");

            RuleFor(x => x.Password)
                .MaximumLength(256)
                .NotEmpty()
                .WithMessage("Password is required.")
                .OverridePropertyName("password");

            RuleFor(x => x.Email)
                .MaximumLength(256)
                .NotEmpty()
                .WithMessage("Email is required.")
                .OverridePropertyName("email");
        }
    }
}