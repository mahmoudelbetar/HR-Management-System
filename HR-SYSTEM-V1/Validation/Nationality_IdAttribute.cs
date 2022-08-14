using HR_SYSTEM_V1.Data;
using HR_SYSTEM_V1.Models;
using System.ComponentModel.DataAnnotations;

namespace HR_SYSTEM_V1.Validation
{
    public class Nationality_IdAttribute : ValidationAttribute
    {
        ApplicationDbContext _db;

        public Nationality_IdAttribute(ApplicationDbContext database)
        {
            _db = database;
        }


        //protected override ValidationResult? IsValid(object? value, ValidationContext ValidationContext)
        //{
        //    int flag = 0;

        //    string? name = value.ToString();
        //    Employee emp = (Employee)ValidationContext.ObjectInstance;
        //    List<Employee> employees = _db.Employees.Where(x => x.Emp_Id == emp.Emp_Id).ToList();

        //    if (courses == null)
        //    {
        //        return ValidationResult.Success;
        //    }
        //    else
        //    {

        //        foreach (Course item in courses)
        //        {

        //            if (item.Name == name)
        //            {
        //                flag = 1;
        //            }
        //        }
        //    }

        //    if (flag == 0)
        //    {
        //        return ValidationResult.Success;
        //    }
        //    else
        //        return new ValidationResult("name Exists in This Department");

        //}
    }
}
