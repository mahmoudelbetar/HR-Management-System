using System.ComponentModel.DataAnnotations;
namespace HR_SYSTEM_V1.Validation
{
    public class HiredateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ( value != null)
            {
                DateTime dateTime = Convert.ToDateTime(value);
                if (dateTime.Year > 2008 && dateTime.Year <= DateTime.Now.Year)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Enter Valid Hire Date ");
            }
            return new ValidationResult("Hire Date is Required");
        }


    }
}
