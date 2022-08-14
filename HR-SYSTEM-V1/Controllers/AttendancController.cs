using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.Repository.InterfaceRepository;
using Microsoft.AspNetCore.Mvc;
using HR_SYSTEM_V1.Data;
using System.Reflection;
using HR_SYSTEM_V1.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HR_SYSTEM_V1.Controllers
{
    public class AttendancController : Controller
    {
        private readonly IAttendance _attendanceRepo;
        private readonly IEmployee _empRepository;
        private readonly IGeneralSetting _generalRepository;
        ApplicationDbContext _db;
        public AttendancController(IAttendance attendanceRepo, IEmployee empRepository, IGeneralSetting general)
        {
            _attendanceRepo = attendanceRepo;
            _empRepository = empRepository;
            _generalRepository = general;


        }

        [HttpGet]
        [Authorize(Permissions.Attendance.View)]
        public IActionResult Index()
        {

            viewDataAndBags();
            return View(_attendanceRepo.getAll());
        }


        [HttpGet]
        [Authorize(Permissions.Attendance.Create)]
        public IActionResult newAttendance()
        {
            viewDataAndBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Attendance.Create)]
        public IActionResult newAttendance(Attendance attendance)
        {
            var allAttendance = _attendanceRepo.getAll();
            var newEmployeeName = _empRepository.getByID(attendance.Emp_Id).Name;    

            if (ModelState.IsValid)
            {
                if (allAttendance.Where(x => x.employee.Name == newEmployeeName && x.Day_Date == DateTime.Today ).FirstOrDefault() == null)
                    _attendanceRepo.add(attendance);
                else
                {
                    ModelState.AddModelError("", "Attendence Already Exist and you Cannot add new Attendence for next or previous Day ");

                    viewDataAndBags();

                    return View("Index", _attendanceRepo.getAll());
                }
                return RedirectToAction("Index");
            }

            viewDataAndBags();
            return View("Index", _attendanceRepo.getAll());
        }

        [HttpGet]
        [Authorize(Permissions.Attendance.Edit)]

        public IActionResult Edit(int id)
        {
            var att = _attendanceRepo.GetById(id);
            ViewBag.flag = 0;
            ViewBag.Attend = att;

            viewDataAndBags();

            return View("Index", _attendanceRepo.getAll()) ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Attendance.Edit)]
        public IActionResult Edit(int id ,Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                var employee = _attendanceRepo.GetById(id).employee;               
                decimal startHours = attendance.StartTimeWork.Value.Hour - employee.StartTime.Hour;
                decimal startMintues = attendance.StartTimeWork.Value.Minute - employee.StartTime.Minute;
                if (startHours > 0 || (startHours == 0 && startMintues > 0))
                {
                    attendance.DiscountTime = Math.Abs((startHours + (startMintues / 60)));
                }
                else
                {
                    attendance.ExtraTime = Math.Abs((startHours + (startMintues / 60)));
                }


                decimal endHours = attendance.EndTimeWork.Value.Hour - employee.EndTime.Hour;
                decimal endMintues = attendance.EndTimeWork.Value.Minute - employee.EndTime.Minute;
                if (endHours > 0 || (endHours == 0 && endMintues > 0))
                {
                    attendance.ExtraTime += Math.Abs(endHours + (endMintues / 60));
                }
                else
                {
                    attendance.DiscountTime += Math.Abs((endHours + (endMintues / 60)));
                }
                _attendanceRepo.Update(id, attendance);
                viewDataAndBags();
                return RedirectToAction("Index");
                }
            else
            {
                var att = _attendanceRepo.GetById(id);
                ViewBag.flag = 0;
                ViewBag.Attend = att;
                ViewData["employees"] = _empRepository.getAll();
                viewDataAndBags();
                return View("Index", _attendanceRepo.getAll());
            }

        }

        [HttpGet]
        [Authorize(Permissions.Attendance.Delete)]
        public IActionResult Delete( int id)
        {
            var att = _attendanceRepo.GetById( id);
            _attendanceRepo.Delete(att);


            viewDataAndBags();

            return View("Index", _attendanceRepo.getAll());
        }

     

        public void viewDataAndBags()
        {
            ViewData["employees"] = _empRepository.getAll();
            Holiday lastHoliday = _generalRepository.getLastHoliday();
            PropertyInfo[] properties = lastHoliday.GetType().GetProperties();
           
            /// list holidayssss
             List<string> days = new List<string>();

            foreach (var item in lastHoliday.GetType().GetProperties())
            {
                bool y;
                bool x = bool.TryParse(item.GetValue(lastHoliday, null).ToString(), out y);
                if (y == true)
                {
                    days.Add(item.Name);
                }
            }
            ViewBag.holidays = days;
        }
    }
}
