using System.ComponentModel.DataAnnotations;

namespace amiliur.annotations.Validation;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class IsNullAttribute : NamedValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }
        return new ValidationResult(ErrorMessage);
    }
}