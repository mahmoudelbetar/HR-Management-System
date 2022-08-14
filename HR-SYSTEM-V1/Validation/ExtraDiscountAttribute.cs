using System.ComponentModel.DataAnnotations;

namespace HR_SYSTEM_V1.Validation
{
    public class ExtraDiscountAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            float ex = float.Parse(value.ToString());
            if (ex > 0)
            return ValidationResult.Success;
            return new ValidationResult("Must be Determined");
        }
    }
}
