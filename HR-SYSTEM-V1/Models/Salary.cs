using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_SYSTEM_V1.Models
{
    public class Salary
    {
        [Key]
        public int Salary_Id { get; set; }
        public int AttendaceDays { get; set; }

        public int AbsentDays { get; set; }
        public decimal OverTimeHours { get; set; }
        public decimal DiscountHours { get; set; }

        public decimal NetSalary { get; set; }
        public string? Month { get; set; }

        [ForeignKey("Employee")]
        public int? Emp_id { get; set; }
        public virtual Employee? Employee { get; set; }

    }
}
