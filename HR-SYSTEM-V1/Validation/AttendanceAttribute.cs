using HR_SYSTEM_V1.Models;
using System.ComponentModel.DataAnnotations;

namespace HR_SYSTEM_V1.Validation
{
    public class AttendanceAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            DateTime endTime = Convert.ToDateTime(value);
            Attendance attendance = (Attendance)validationContext.ObjectInstance;
            try
            {
                if (attendance.StartTimeWork.Value == null)
                {
                    return new ValidationResult("End Time Can Not Be Before Start Time ");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult("End Time Can Not Be Before Start Time ");
            }





            if ((DateTime.Compare(endTime, attendance.StartTimeWork.Value) > 0 && value!= null) || value == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("End Time Can Not Be Before Start Time ");
            }



        }


    }
}
