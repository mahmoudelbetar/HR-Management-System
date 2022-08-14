using HR_SYSTEM_V1.Constants;
using HR_SYSTEM_V1.Data;
using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.Repository.InterfaceRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_SYSTEM_V1.Controllers
{
    public class SalaryReportController : Controller
    {
        ISalaryReport _SalaryReport;
        IGeneralSetting _generalSetting;
        ApplicationDbContext _db;
        public SalaryReportController(ISalaryReport report, IGeneralSetting gn ,ApplicationDbContext data)
        {
            _SalaryReport = report;
            _generalSetting = gn;
            _db = data;
        }


        [HttpGet]
        [Authorize(Permissions.Salaryreport.View)]
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                if (_generalSetting.checkGeneralSetting()  || _db.Attendances.ToList().Count == 0 || _SalaryReport.getAll() == null)
                {
                    ModelState.AddModelError("", "No General Setting Exist Or No Attendences Till Now");
                    return View(new List<Salary>());
                }
                else
                {
                    _SalaryReport.AddSalary();
                    return View(_SalaryReport.getAll());
                }
            }
            return View(_SalaryReport.getAll());
        }
    }
}
