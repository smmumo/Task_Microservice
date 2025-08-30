


using AuthService.Domain.Core;

namespace AuthService.Domain.Errors;

/// <summary>
/// Contains the domain errors.
/// </summary>
public static class DomainErrors
{
    /// <summary>
    /// Contains the user errors.
    /// </summary>
    public static class User
    {
        public static Error NotFound => new Error("User.NotFound", "The user with the specified identifier was not found.");
        // public static Error NotFound => new Error("User.NotFound", "The user with the specified identifier was not found.");
        public static Error ErrorCreatingUser => new Error("User.NotCreated", "Error occured While creating this user.");

        public static Error DuplicateEmail => new Error("User.DuplicateEmail", "The specified email is already in use.");
        public static Error EmailNotUnique => new Error("User.EmailNotUnique", "The specified email is not unique.");
        public static Error InvalidInput => new Error("User.InvalidInput", "The user input is invalid.");
    }

    /// <summary>
    /// Contains the name errors.
    /// </summary>
    public static class Name
    {
        public static Error NullOrEmpty => new Error("Name.NullOrEmpty", "The name is required.");

        public static Error LongerThanAllowed => new Error("Name.LongerThanAllowed", "The name is longer than allowed.");
    }



    /// <summary>
    /// Contains the email errors.
    /// </summary>
    public static class Email
    {
        public static Error NullOrEmpty => new Error("Email.NullOrEmpty", "The email is required.");

        public static Error LongerThanAllowed => new Error("Email.LongerThanAllowed", "The email is longer than allowed.");

        public static Error InvalidFormat => new Error("Email.InvalidFormat", "The email format is invalid.");
    }

    /// <summary>
    /// Contains the password errors.
    /// </summary>
    public static class Password
    {
        public static Error NullOrEmpty => new Error("Password.NullOrEmpty", "The password is required.");

        public static Error TooShort => new Error("Password.TooShort", "The password is too short.");

        public static Error DoNotMatch => new Error("Password.DoNotMatch", "The password and confirmation password do not match.");

        public static Error MissingUppercaseLetter => new Error(
            "Password.MissingUppercaseLetter",
            "The password requires at least one uppercase letter.");

        public static Error MissingLowercaseLetter => new Error(
            "Password.MissingLowercaseLetter",
            "The password requires at least one lowercase letter.");

        public static Error MissingDigit => new Error(
            "Password.MissingDigit",
            "The password requires at least one digit.");

        public static Error MissingNonAlphaNumeric => new Error(
            "Password.MissingNonAlphaNumeric",
            "The password requires at least one non-alphanumeric.");
    }

    /// <summary>
    /// Contains general errors.
    /// </summary>
    public static class General
    {
        public static Error UnProcessableRequest => new Error(
            "General.UnProcessableRequest",
            "The server could not process the request.");

        public static Error NotFound => new Error(
           "General.NotFound",
           "The requested resource was not found.");

        public static Error ServerError => new Error("General.ServerError", "The server encountered an unrecoverable error.");
    }

    /// <summary>
    /// Contains the authentication errors.
    /// </summary>
    public static class Authentication
    {
        public static Error InvalidEmailOrPassword => new Error(
            "Authentication.InvalidEmailOrPassword",
            "The specified email or password are incorrect.");

        public static Error InvalidToken => new Error(
                "Authentication.IInvalidToken",
                "The specified token is invalid.");
    }
        
    
       
}

