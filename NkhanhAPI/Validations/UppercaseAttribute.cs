using System.ComponentModel.DataAnnotations;

namespace NkhanhAPI.Validations
{
    public class UppercaseAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string str && str != str.ToUpper())
            {
                return new ValidationResult("Code must be uppercase.");
            }

            return ValidationResult.Success;
        }
    }
}
