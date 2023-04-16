using System.ComponentModel.DataAnnotations;
using amiliur.annotations.Extensions;

namespace amiliur.annotations.Validation;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class GreaterAttribute : NamedValidationAttribute
{
    public string OtherProperty { get; set; } = "";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var otherPropertyInfo = validationContext.GetObjectProperty(OtherProperty);
        var otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

        if (value == null || otherPropertyValue == null)
            return ValidationResult.Success;

        if (value is not IComparable comparableValue)
            return new ValidationResult("Value must implement IComparable.");

        return comparableValue.CompareTo(otherPropertyValue) > 0
            ? ValidationResult.Success
            : new ValidationResult(ErrorMessage);
    }

   
}