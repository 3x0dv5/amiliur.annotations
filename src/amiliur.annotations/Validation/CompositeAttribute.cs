using System.ComponentModel.DataAnnotations;
using amiliur.annotations.Extensions;

namespace amiliur.annotations.Validation;

public enum CompositeType
{
    Or,
    And
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class CompositeAttribute : ValidationAttribute
{
    public CompositeType Type { get; set; }
    public string[]? AttributeList { get; set; }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.MemberName != null)
        {
            var property = validationContext.GetObjectProperty(validationContext.MemberName);
            List<NamedValidationAttribute> propertyAttributes;
            if (AttributeList != null && AttributeList.Any())
            {
                propertyAttributes = property.GetCustomAttributes(typeof(NamedValidationAttribute), false)
                    .OfType<NamedValidationAttribute>()
                    .Where(attr => AttributeList.Contains(attr.Name))
                    .ToList();

                if (propertyAttributes.Count != AttributeList.Length)
                {
                    throw new ArgumentException("Some attribute names provided in AttributeList were not found.");
                }
            }
            else
            {
                propertyAttributes = property.GetCustomAttributes(typeof(NamedValidationAttribute), false)
                    .OfType<NamedValidationAttribute>()
                    .ToList();
            }

            var validationResults = propertyAttributes
                .Select(attr => attr.GetValidationResult(value, validationContext))
                .ToList();

            return ValidationResultForType(validationResults);
        }

        return new ValidationResult("MemberName must be provided.");
    }

    private ValidationResult? ValidationResultForType(List<ValidationResult?> validationResults)
    {
        if (Type == CompositeType.And)
        {
            if (validationResults.All(r => r == ValidationResult.Success))
            {
                {
                    return ValidationResult.Success;
                }
            }
        }

        else if (Type == CompositeType.Or)
        {
            if (validationResults.Any(r => r == ValidationResult.Success))
            {
                {
                    return ValidationResult.Success;
                }
            }
        }

        return new ValidationResult(ErrorMessage);
    }
}

public class OrCompositeAttribute : CompositeAttribute
{
    public OrCompositeAttribute()
    {
        Type = CompositeType.Or;
    }
}

public class AndCompositeAttribute : CompositeAttribute
{
    public AndCompositeAttribute()
    {
        Type = CompositeType.And;
    }
}