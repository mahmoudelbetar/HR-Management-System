using HR_SYSTEM_V1.Data;
using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.Repository;
using HR_SYSTEM_V1.Repository.InterfaceRepository;
using HR_SYSTEM_V1.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HR_SYSTEM_V1.Repository.ClassRepository
{
    public class SalaryReportRepository : ISalaryReport
    {
        ApplicationDbContext _db;
        IEmployee _empRepo;
        IAttendance _attendRepo;
        IGeneralSetting _generalRepo;
        IAttendance _attendanceRepo;
        public SalaryReportRepository(ApplicationDbContext data, IEmployee empRepo , IAttendance attendance ,IGeneralSetting gen , IAttendance attendance1)
        {
            _db = data;
            _empRepo = empRepo;
            _attendRepo = attendance;
            _generalRepo = gen;
            _attendanceRepo = attendance1;
        }






        public List<Salary> getAll()
        {
            return _db.Salaries.Include(x => x.Employee).ToList();
        }



        public void AddSalary()
        {

            var employees = _db.Employees.Include(x=>x.salaries).Include(x=>x.attendances).ToList();
            DateTime startDate;
            if (_db.Attendances.ToList().Count == 0) { }
            var mon = _db.Attendances.OrderBy(x => x.Day_Date.Year).ThenBy(x => x.Day_Date.Month).FirstOrDefault().Day_Date.Month;

            startDate = new DateTime(2022, mon, 1);
            DateTime endDate = DateTime.Today;
            for (DateTime dt = startDate; dt <= endDate; dt = dt.AddMonths(1))
            {

                var editMonth = dt.Month;
                DiscountExtra lastExtraDiscount = null;
                Holiday lastHoliday = null;
                if (editMonth < _db.DiscountExtras.FirstOrDefault().CreatedDate.Month)
                {
                    lastExtraDiscount = _db.DiscountExtras.OrderBy(x => x.CreatedDate.Month).ThenByDescending(x => x.CreatedDate.Day).LastOrDefault();
                    lastHoliday = _db.Holidays.OrderBy(x => x.Edit_Day_General.Month).ThenByDescending(x => x.Edit_Day_General.Day).LastOrDefault();
                }
                else
                {
                    lastExtraDiscount = _db.DiscountExtras.Where(x => x.CreatedDate.Month <= editMonth).OrderByDescending(x=>x.CreatedDate.Month).ThenByDescending(x=>x.CreatedDate.Day).ThenByDescending(x=>x.CreatedDate.Hour).ThenByDescending(x=>x.CreatedDate.Minute).FirstOrDefault();
                    lastHoliday = _db.Holidays.Where(x => x.Edit_Day_General.Month <= editMonth).OrderByDescending(x => x.Edit_Day_General.Month).ThenByDescending(x => x.Edit_Day_General.Day).ThenByDescending(x => x.Edit_Day_General.Hour).ThenByDescending(x => x.Edit_Day_General.Minute).FirstOrDefault();
                }

                var holidaycount = 0;
                PropertyInfo[] properties = lastHoliday.GetType().GetProperties();
                foreach (var item in lastHoliday.GetType().GetProperties())
                {
                    bool y;
                    bool x = bool.TryParse(item.GetValue(lastHoliday, null).ToString(), out y);
                    if (y == true)
                    {
                        holidaycount++;
                    }
                }


                foreach (var emp in employees)
                {

                    List<Salary> checkSalary = emp.salaries.Where(x => x.Month == dt.Month.ToString()).ToList();
                    if (checkSalary.Count() != 0 && dt.Month != DateTime.Now.Month) continue;
                    if(dt.Month == DateTime.Now.Month)
                    {
                        var salary = emp.salaries.Where(x=>x.Month ==dt.Month.ToString()).FirstOrDefault();
                        if(salary == null) { }
                        else
                        {
                            _db.Remove(salary);
                            _db.SaveChanges();
                        }
                    }
                    Salary Salary = new Salary();
                    Salary.Month = dt.Month.ToString();
                    Salary.Emp_id = emp.Emp_Id;
                    int month = int.Parse(dt.ToString("MM"));
                    int year = int.Parse(dt.ToString("yyyy"));
                    int monthWork = DateTime.DaysInMonth(year, month) - (holidaycount * 4);
                    Salary.AttendaceDays = (_db.Attendances.Where(x => x.Emp_Id == emp.Emp_Id && x.Day_Date.Month == dt.Month && x.Absent == false).Count());
                    Salary.AbsentDays = DateTime.DaysInMonth(year, month) - Salary.AttendaceDays - holidaycount * 4;

                    var dailySalary = emp.Salary / monthWork;
                    var hourlySalary = emp.Salary / monthWork / (emp.EndTime.Hour - emp.StartTime.Hour);
                    Salary.OverTimeHours = emp.attendances.Where(x => x.Day_Date.Month == dt.Month).Select(x => x.ExtraTime).Sum();
                    Salary.DiscountHours = emp.attendances.Where(x => x.Day_Date.Month == dt.Month).Select(x => x.DiscountTime).Sum();

                    if (lastExtraDiscount.Type == ExtraDiscountType.Hours)
                    {
                        Salary.NetSalary = Math.Round(emp.Salary + (Salary.OverTimeHours * lastExtraDiscount.Extra - Salary.DiscountHours * lastExtraDiscount.Discount) * hourlySalary - ((monthWork - Salary.AttendaceDays) * dailySalary), 2);
                    }
                    else
                    {
                        Salary.NetSalary = Math.Round(emp.Salary + (Salary.OverTimeHours * lastExtraDiscount.Extra - Salary.DiscountHours * lastExtraDiscount.Discount) - ((monthWork - Salary.AttendaceDays) * dailySalary), 2);
                    }

                    _db.Add(Salary);
                    _db.SaveChanges();

                }
            }
        }














                public List<SalaryReportViewModel> CalcSalary()
        {
            List<SalaryReportViewModel> SalaryReportList = new List<SalaryReportViewModel>();
            var employees = _empRepo.getAll();
            var lastExtraDiscount = _generalRepo.getLastExtraDiscount();
            Holiday lastHoliday = _generalRepo.getLastHoliday();
            var holidaycount = 0;
            PropertyInfo[] properties = lastHoliday.GetType().GetProperties();
            foreach (var item in lastHoliday.GetType().GetProperties())
            {
                bool y;
                bool x = bool.TryParse(item.GetValue(lastHoliday, null).ToString(), out y);
                if (y == true)
                {
                    holidaycount++;
                }
            }

            DateTime startDate = new DateTime (2022 , 6 , 1);
            DateTime endDate = DateTime.Today;
            for (DateTime dt = startDate; dt <= endDate; dt = dt.AddMonths(1))
            {
                        

                foreach (var emp in employees)
                {
                    SalaryReportViewModel SalaryReport = new SalaryReportViewModel();
                    SalaryReport.Id = emp.Emp_Id;
                    SalaryReport.Month = dt.Month.ToString();
                    SalaryReport.EmployeeName = emp.Name;
                    SalaryReport.Phone = emp.Phone;
                    SalaryReport.Salary = emp.Salary;
                    int month = int.Parse(dt.ToString("MM"));
                    int year = int.Parse(dt.ToString("yyyy"));
                    int monthWork = DateTime.DaysInMonth(year, month) - (holidaycount * 4);
                    SalaryReport.AttendaceDays = (_db.Attendances.Where(x => x.Emp_Id == emp.Emp_Id && x.Day_Date.Month == dt.Month && x.Absent == false).Count());
                    SalaryReport.AbsentDays = DateTime.DaysInMonth(year, month) - SalaryReport.AttendaceDays - holidaycount*4;

                    var dailySalary = emp.Salary / monthWork;
                    var hourlySalary = emp.Salary /monthWork / (emp.EndTime.Hour - emp.StartTime.Hour);
                    SalaryReport.OverTimeHours = (_db.Attendances.Where(x => x.Emp_Id == emp.Emp_Id && x.Day_Date.Month == dt.Month).Select(x => x.ExtraTime).Sum());
                    SalaryReport.DiscountHours = (_db.Attendances.Where(x => x.Emp_Id == emp.Emp_Id && x.Day_Date.Month == dt.Month).Select(x => x.DiscountTime).Sum());
                    
                    if(lastExtraDiscount.Type == ExtraDiscountType.Hours)
                    {
                        SalaryReport.NetSalary = Math.Round(emp.Salary + (SalaryReport.OverTimeHours * lastExtraDiscount.Extra - SalaryReport.DiscountHours * lastExtraDiscount.Discount) * hourlySalary - ((monthWork - SalaryReport.AttendaceDays) * dailySalary), 2);
                    }
                    else
                    {
                        SalaryReport.NetSalary = Math.Round(emp.Salary + (SalaryReport.OverTimeHours * lastExtraDiscount.Extra - SalaryReport.DiscountHours * lastExtraDiscount.Discount) - ((monthWork - SalaryReport.AttendaceDays) * dailySalary ) , 2);
                    }

                    if (SalaryReport.AttendaceDays == 0) { }
                    else { SalaryReportList.Add(SalaryReport); }
                    
                }
                 
            }
            return SalaryReportList;

        }

    }
}
