using HR_SYSTEM_V1.Models;
using System.ComponentModel.DataAnnotations;

namespace HR_SYSTEM_V1.Validation
{
    public class EmployeeWorkingTimeAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            DateTime endTime = Convert.ToDateTime(value);
            Employee employee = (Employee)validationContext.ObjectInstance;

            if (DateTime.Compare(endTime, employee.StartTime) > 0)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("End Time Can Not Be Before Start Time ");

        }

    }
}
