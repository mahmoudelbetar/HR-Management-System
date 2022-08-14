using Microsoft.AspNetCore.Mvc;

namespace HR_SYSTEM_V1.Controllers
{
    public class RemoteController : Controller
    {
        public IActionResult CheckTime(DateTime EndTime, DateTime StartTime)
        {
            if (DateTime.Compare(StartTime, EndTime) < 0)
            {
                return Json(true);
            }
            return Json(false);
        }

        public IActionResult CheckBirthDate(DateTime BirthDate)
        {
            if (DateTime.Now.Year - BirthDate.Year > 20)
            {
                return Json(true);
            }
            return Json(false);
        }

        public IActionResult CheckHireDate( DateTime HireDate) 
        {
            
            if (HireDate.Year > 2008)
            {
                return Json(true);
            }
            else
            return Json(false);
        
        
        }




    }
}
