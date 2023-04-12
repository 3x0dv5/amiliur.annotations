using System.ComponentModel.DataAnnotations;

namespace amiliur.annotations.Validation;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class EqualAttribute : NamedValidationAttribute
{
    public string OtherProperty { get; set; } = "";

   

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);

        if (otherPropertyInfo == null)
        {
            return new ValidationResult($"Property '{OtherProperty}' not found.");
        }

        var otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

        if (value == null || otherPropertyValue == null)
            return ValidationResult.Success;

        return value.Equals(otherPropertyValue)
            ? ValidationResult.Success
            : new ValidationResult(ErrorMessage);
    }
}