using HR_SYSTEM_V1.Data;
using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.Repository.InterfaceRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HR_SYSTEM_V1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISalaryReport _salaryReport;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ISalaryReport sr, ApplicationDbContext data)
        {
            _logger = logger;
            _salaryReport = sr;
            _db = data;
        }


        public IActionResult Index()
        {
            //if (_db.DiscountExtras.ToList().Count == 0 || _db.Attendances.ToList().Count == 0) { }
            //else
            //{
            //    if (DateTime.Now.Day > 12)
            //    {
            //        _salaryReport.AddSalary();
            //    }
            //}

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}