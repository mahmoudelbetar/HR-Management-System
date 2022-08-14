using HR_SYSTEM_V1.Constants;
using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using permissions.View_Model;

namespace HR_SYSTEM_V1.Controllers
{
    public class UsersController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        [Authorize(Permissions.Users.View)]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users
               .Select(user => new UserViewModel { Id = user.Id, UserName = user.UserName, Roles = _userManager.GetRolesAsync(user).Result })
               .ToListAsync();
            return View(users);
        }


        [Authorize(Permissions.Users.Edit)]
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();

            var viewModel = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(role => new CheckBoxViewModel
                {
                    DisplayValue = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Users.Edit)]

        public async Task<IActionResult> UpdateRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelected).Select(r => r.DisplayValue));

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Permissions.Users.Create)]
        public IActionResult NewUser()
        {
            var roles = _roleManager.Roles.ToList();
            List<AddRoleViewModel> rolesList = new List<AddRoleViewModel>();

            foreach (var role in roles)
            {
                AddRoleViewModel roleVM = new AddRoleViewModel();

                roleVM.id = role.Id;
                roleVM.name = role.Name;

                rolesList.Add(roleVM);
            }

            return View(rolesList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Users.Create)]

        public async Task<IActionResult> NewUser(AddUserViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = newUser.userName;
                user.PasswordHash = newUser.password;

                if (user != null && newUser.password != null)
                {
                    var result = await _userManager.CreateAsync(user, newUser.password);
                    if (result.Succeeded && result != null)
                    {
                        var result2 = await _userManager.AddToRoleAsync(user, newUser.role);

                        if (result2.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        foreach (var err in result2.Errors)
                        {
                            ModelState.AddModelError("", err.Description);
                        }
                    }
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
                return RedirectToAction("NewUser");
            }

            return View(newUser);
        }




        [Authorize(Permissions.Users.Delete)]
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");

        }





    }
}
