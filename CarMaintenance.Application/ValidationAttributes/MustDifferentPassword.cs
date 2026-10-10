using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Application.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MustDifferentPassword(string otherProperty) : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            var otherValue = context.ObjectType
                .GetProperty(otherProperty)?
                .GetValue(context.ObjectInstance);

            return value?.ToString() == otherValue?.ToString()
                ? new ValidationResult("The new password must be different from the current password.")
                : ValidationResult.Success;
        }
    }
}