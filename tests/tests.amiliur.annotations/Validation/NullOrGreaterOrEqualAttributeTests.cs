using System.ComponentModel.DataAnnotations;
using amiliur.annotations.Validation;

namespace tests.amiliur.annotations.Validation;

public class CompositeAttributeTests
{
    class CompareClsTest
    {
        public DateOnly? Date1 { get; set; }

        [OrComposite(ErrorMessage = "Date2 should not be lower than Date1")]
        [IsNull]
        [Greater(OtherProperty = nameof(Date1))]
        [Equal(OtherProperty = nameof(Date1))]
        public DateOnly? Date2 { get; set; }

        public CompareClsTest(DateOnly? date1, DateOnly? date2)
        {
            Date1 = date1;
            Date2 = date2;
        }
    }

    private static IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model);
        Validator.TryValidateObject(model, validationContext, validationResults, true);
        return validationResults;
    }

    [Fact]
    public void TestNull()
    {
        var cls = new CompareClsTest(new DateOnly(2021, 1, 1), null);
        var result = ValidateModel(cls);

        Assert.Empty(result);
    }

    [Fact]
    public void NullOrGreaterOrEqualAttribute_Validation_Failure()
    {
        var model = new CompareClsTest(DateOnly.Parse("2023-01-01"), DateOnly.Parse("2022-12-31"));
        var validationResults = ValidateModel(model);
        Assert.NotEmpty(validationResults);
        Assert.Contains(validationResults, vr => vr.ErrorMessage == "Date2 should not be lower than Date1");
    }
    
    [Fact]
    public void NullOrGreaterOrEqualAttribute_Validation_Pass()
    {
        var model = new CompareClsTest( DateOnly.Parse("2022-12-31"), DateOnly.Parse("2023-01-01"));
        var validationResults = ValidateModel(model);
        Assert.Empty(validationResults);
    }
}