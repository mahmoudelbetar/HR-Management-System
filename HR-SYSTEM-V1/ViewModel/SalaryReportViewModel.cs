namespace HR_SYSTEM_V1.ViewModel
{
    public class SalaryReportViewModel
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Phone { get; set; }

        public decimal Salary { get; set; }

        public int AttendaceDays { get; set; }

        public int AbsentDays { get; set; }
        public decimal OverTimeHours { get; set; }
        public decimal DiscountHours { get; set; }

        public decimal NetSalary { get; set; }
        public string Month { get; set; }
    }
}
