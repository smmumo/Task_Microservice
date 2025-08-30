using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Contracts;
using AuthService.Domain;
using FluentValidation;

namespace AuthService.Validation
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(4)
                .WithMessage("Password must be at least 4 characters long.");

           RuleFor(user => user.ConfirmPassword)
               .NotEmpty()
               .WithMessage("Confirm Password is required.")
               .Equal(user => user.Password)
               .WithMessage("Passwords do not match.");
        }
    }
}