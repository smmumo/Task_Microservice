using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Domain.Core.Result;
using AuthService.Domain.Errors;

namespace AuthService.Domain
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public static Result<UserEntity> Create(string name, string email, string password)
        {
            var nameValidation = ValidateName(name);
            if (nameValidation.IsFailure) return Result.Failure<UserEntity>(nameValidation.Error);

            var emailValidation = ValidateEmail(email);
            if (emailValidation.IsFailure) return Result.Failure<UserEntity>(emailValidation.Error);

            var passwordValidation = ValidatePassword(password);
            if (passwordValidation.IsFailure) return Result.Failure<UserEntity>(passwordValidation.Error);

            var confirmPassword = ConfirmPassword(password, password);
            if (confirmPassword.IsFailure) return Result.Failure<UserEntity>(confirmPassword.Error);
            
            UserEntity user = new()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = password
            };
            return Result.Success(user);
        }

        public static Result ConfirmPassword(string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                return Result.Failure(DomainErrors.Password.DoNotMatch);
            }

            return Result.Success();
        }

        public static Result ValidateName(string nameToCheck)
        {
            if (string.IsNullOrEmpty(nameToCheck))
            {
                return Result.Failure(DomainErrors.Name.NullOrEmpty);
            }

            return Result.Success();
        }

        public static Result ValidateEmail(string emailToCheck)
        {
            if (string.IsNullOrEmpty(emailToCheck))
            {
                return Result.Failure(DomainErrors.Email.NullOrEmpty);
            }

            return Result.Success();
        }

        public static Result ValidatePassword(string passwordToCheck)
        {
            if (string.IsNullOrEmpty(passwordToCheck))
            {
                return Result.Failure(DomainErrors.Password.NullOrEmpty);
            }

            return Result.Success();
        }

        public void UpdatePassword(string newPassword)
        {
            Password = newPassword;
        }
    }
}