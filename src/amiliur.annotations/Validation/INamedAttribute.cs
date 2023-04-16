using System.ComponentModel.DataAnnotations;
using amiliur.annotations.Extensions;

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
    // protected bool IsUsedInCompositeAttribute(ValidationContext validationContext)
    // {
    //     if (validationContext.MemberName != null)
    //     {
    //         var property = validationContext.GetObjectProperty(validationContext.MemberName);
    //         return property
    //             .GetCustomAttributes(typeof(CompositeAttribute), false)
    //             .OfType<CompositeAttribute>()
    //             .Any(attr => attr.AttributeList != null && attr.AttributeList.Contains(Name));
    //     }
    //     return false;
    // }
}