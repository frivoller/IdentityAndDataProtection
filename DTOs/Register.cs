using System.ComponentModel.DataAnnotations;

namespace IdentityAndDataProtection.DTOs

    public sealed record Register
    {

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters long.")]
        public string UserName { get; init; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string EmailAddress { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; init; } = string.Empty;

        public IEnumerable<string> Validate()
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this);
            bool isValid = Validator.TryValidateObject(this, validationContext, validationResults, true);

            return isValid ? Enumerable.Empty<string>() : validationResults.Select(v => v.ErrorMessage!);
        }
    }
}