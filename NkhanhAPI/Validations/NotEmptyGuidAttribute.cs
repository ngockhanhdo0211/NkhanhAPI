using System;
using System.ComponentModel.DataAnnotations;

namespace NkhanhAPI.Validations
{
    public class NotEmptyGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            if (value is Guid guid)
            {
                if (guid == Guid.Empty)
                {
                    return new ValidationResult(
                        $"{validationContext.MemberName} cannot be empty GUID");
                }
            }

            return ValidationResult.Success;
        }
    }
}
