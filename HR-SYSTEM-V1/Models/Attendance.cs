using HR_SYSTEM_V1.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_SYSTEM_V1.Models
{
    public class Attendance
    {
        [Key]
        public int Attend_Id { get; set; }
        public decimal ExtraTime { get; set; }
        public decimal DiscountTime { get; set; }
        [Required]
        public DateTime Day_Date { get; set; } = DateTime.Now;
        [Required]
        public DateTime? StartTimeWork { get; set; }

        [Attendance]
        public DateTime? EndTimeWork { get; set; }
        [DefaultValue(0)]
        public bool Absent { get; set; } = true;
        [ForeignKey("employee")]
        public int Emp_Id { get; set; }
        public virtual Employee employee { get; set; }



    }
}
