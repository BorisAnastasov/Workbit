using IbanNet;
using System.ComponentModel.DataAnnotations;

namespace Workbit.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidIbanAttribute : ValidationAttribute
    {
        protected override System.ComponentModel.DataAnnotations.ValidationResult? IsValid(
                                        object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return new System.ComponentModel.DataAnnotations.ValidationResult("IBAN is required.");


            var validator = (IIbanValidator)validationContext.GetService(typeof(IIbanValidator))!;
            var result = validator.Validate(value.ToString()!);

            if (result.IsValid)
                return System.ComponentModel.DataAnnotations.ValidationResult.Success;

            return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid IBAN format.");
        }
    }
}
