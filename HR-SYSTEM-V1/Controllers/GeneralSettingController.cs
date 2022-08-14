using Microsoft.AspNetCore.Mvc;
using HR_SYSTEM_V1.Data;
using HR_SYSTEM_V1.Repository.InterfaceRepository;
using HR_SYSTEM_V1.ViewModel;
using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HR_SYSTEM_V1.Controllers
{
    public class GeneralSettingController : Controller
    {
        IGeneralSetting gs;
        
        public GeneralSettingController(IGeneralSetting generalSetting)
        {
            gs = generalSetting;
        }

        [Authorize(Permissions.GenralSettings.View)]

        public IActionResult Index()
        {
            Holiday lastHoliday =  gs.getLastHoliday();
            DiscountExtra lastDiscountExtra = gs.getLastExtraDiscount();
            var general = new GeneralSettingViewModel()
            {
                Discount = lastDiscountExtra.Discount,
                Extra = lastDiscountExtra.Extra,
                ExtraDiscountType = lastDiscountExtra.Type,
                Saturday = lastHoliday.Saturday,
                Sunday = lastHoliday.Sunday,
                Monday = lastHoliday.Monday,
                Tuesday = lastHoliday.Tuesday,
                Wednesday = lastHoliday.Wednesday,
                Thursday = lastHoliday.Thursday,
                Friday = lastHoliday.Friday
            };

            return View(general);
        }


        [Authorize(Permissions.GenralSettings.Create)]

        public IActionResult addGeneralSetting ( GeneralSettingViewModel gsvm)
        {
            if (ModelState.IsValid)
            {
                var extra_discount = new DiscountExtra()
                {
                    CreatedDate = DateTime.Now,
                    Discount = gsvm.Discount,
                    Extra = gsvm.Extra,
                    Type = gsvm.ExtraDiscountType,
                };
                


                if (gsvm.Saturday || gsvm.Sunday || gsvm.Monday || gsvm.Tuesday || gsvm.Wednesday || gsvm.Thursday || gsvm.Friday)
                {
                    var holidays = new Holiday()
                    {
                        Edit_Day_General = DateTime.Now,
                        Saturday = gsvm.Saturday,
                        Sunday = gsvm.Sunday,
                        Monday = gsvm.Monday,
                        Tuesday = gsvm.Tuesday,
                        Wednesday = gsvm.Wednesday,
                        Thursday = gsvm.Thursday,
                        Friday = gsvm.Friday
                    };

                    gs.addHolidays(holidays);
                    gs.addDiscountExtra(extra_discount);
                }
                else
                {
                    ModelState.AddModelError("", "Holidays Must be Selected");
                    return View("Index", gsvm);
                }


                return RedirectToAction("Index");

            }
            else return View("Index", gsvm); 
        }



    }
}
