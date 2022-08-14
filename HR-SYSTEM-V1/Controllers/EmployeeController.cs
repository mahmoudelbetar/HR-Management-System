using HR_SYSTEM_V1.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HR_SYSTEM_V1.Repository.ClassRepository;
using HR_SYSTEM_V1.Data;
using HR_SYSTEM_V1.Repository.InterfaceRepository;
using HR_SYSTEM_V1.Models;

namespace HR_SYSTEM_V1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployee employeeRepo;
        public EmployeeController(IEmployee employee)
        {
            employeeRepo = employee;
        }


        //---------------------- Get All -----------------
        [Authorize(Permissions.Employee.View)]
        public IActionResult Index()
        {
            return View(employeeRepo.getAll());
        }



        //---------------------- Add -----------------

        [Authorize(Permissions.Employee.Create)]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Permissions.Employee.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Employee emp)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {

                    byte[] p = null;
                    using (var fs = files[0].OpenReadStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            p = ms.ToArray();
                        }
                    }
                    emp.Emp_Image = p;
                }

                if(employeeRepo.Check_National_Id(emp.Emp_Id , emp.national_Id))
                {
                    ModelState.AddModelError("", "National Id Already Exists");
                    return View(emp);
                }
                else
                {
                    employeeRepo.Add(emp);
                }


                //if (employeeRepo.Check_National_Id(emp.national_Id))
                //{
                //    ModelState.AddModelError("", "National Id Already Exists");
                //    return View(emp);
                //}
                //else
                //{
                //    employeeRepo.Add(emp);
                //}


                return RedirectToAction("Index");
            }
            return View(emp);
        }



        //--------------------- Update ---------------


        [Authorize(Permissions.Employee.Edit)]
        [HttpGet]
        public IActionResult openEditPage(int id)
        {
            Employee emp = employeeRepo.getByID(id);
            return View(emp);
        }


        [Authorize(Permissions.Employee.Edit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee emp)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {

                    byte[] p = null;
                    using (var fs = files[0].OpenReadStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            p = ms.ToArray();
                        }
                    }
                    emp.Emp_Image = p;
                }

                if (employeeRepo.Check_National_Id(emp.Emp_Id , emp.national_Id))
                {
                    ModelState.AddModelError("", "National Id Already Exists");
                    return View("openEditPage", emp);
                }
                else
                {
                    employeeRepo.updateEmployee(emp);
                }
                //if (employeeRepo.Check_National_Id(emp.national_Id))
                //{
                //    ModelState.AddModelError("", "National Id Already Exists");
                //    return View("openEditPage", emp);
                //}
                //else
                //{
                //    employeeRepo.updateEmployee(emp);

                //}
                return RedirectToAction("Index");
            }
            else return View("openEditPage", emp );
            
        }


        //------------------------- Delete -------------------------

        [Authorize(Permissions.Employee.Delete)]
        public IActionResult deleteEmp(int id)
        {
            employeeRepo.delete(id);

            return RedirectToAction("Index");
        }


    }
}
