using System.ComponentModel.DataAnnotations;

namespace HR_SYSTEM_V1.Validation
{
    public class AgeAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime birthdate = Convert.ToDateTime(value);
            if (DateTime.Now.Year - birthdate.Year > 20 && DateTime.Now.Year - birthdate.Year < 50)
                return ValidationResult.Success;
            else
                return new ValidationResult("Age Must be Between 20 - 50");
        }

    }
}
