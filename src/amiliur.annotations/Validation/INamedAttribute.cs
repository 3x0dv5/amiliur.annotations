using System.ComponentModel.DataAnnotations;

namespace amiliur.annotations.Validation;

public abstract class NamedValidationAttribute : Attribute
{
    public string? GroupName { get; set; }
    public string? Name { get; set; }
    public string? ErrorMessage { get; set; }
    protected virtual ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        return ValidationResult.Success;
    }
    public ValidationResult? GetValidationResult(object? value, ValidationContext validationContext)
    {
        return IsValid(value, validationContext);
    }
  
}