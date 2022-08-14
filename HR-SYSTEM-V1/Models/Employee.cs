using HR_SYSTEM_V1.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_SYSTEM_V1.Models
{
    public class Employee
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Emp_Id { get; set; }


        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,80}$", ErrorMessage = "Invalid Name")]
        //[StringLength(100 ,MinimumLength =2 ,ErrorMessage ="minimum 1 char and maximum 100 char")]
        public string Name { get; set; }


        [Required]
        [Range(2000 ,50000,ErrorMessage ="Salary Must Between 2000 - 50000")]
        public decimal Salary { get; set; }


        [Required]
        [RegularExpression ("[0-9]{14}" , ErrorMessage ="National Id Must be 14 Number")]
        public string national_Id { get; set; }


        [Required]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }


        [Required]
        [DataType(DataType.Time)]
        [EmployeeWorkingTime]
        //[Remote("CheckTime", "Remote", AdditionalFields = "StartTime", ErrorMessage ="End Time Must Be After Start Time")]
        public DateTime EndTime { get; set; }
        
        
        [Required]
        [StringLength(50)]
        public string Nationalty { get; set; }


        [Required]
        public Gender Gender { get; set; }


        public byte[]? Emp_Image { get; set; }


        [Required]
        [Age]
        //[Remote("CheckBirthDate" , "Remote", ErrorMessage ="Age Must Be More Than 20")]
       // [DataType(DataType.Date)]
        public DateTime Birth_Date { get; set; }


        [Required]
        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$", ErrorMessage = "Enter valid Phone Number")]
        //[RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Phone { get; set; }


        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_\\.-]+@[a-zA-Z]+(\.com)", ErrorMessage = "Invalid Email")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }


        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Enter Valid Address")]
        public string Address { get; set; }


        [Required]
        [Hiredate]
       // [DataType(DataType.Date)]
        public DateTime Hire_Date { get; set; }

        public string? Note { get; set; }

        public List<Attendance>? attendances { get; set; }

        public virtual List<Salary>? salaries { get; set; }


    }



   public enum Gender{

        Male,
        Female ,
        

    }



}
