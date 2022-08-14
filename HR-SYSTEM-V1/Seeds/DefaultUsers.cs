using HR_SYSTEM_V1.Constants;
using HR_SYSTEM_V1.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HR_SYSTEM_V1.Seeds
{
    public static class DefaultUsers
    {

        public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> usermanger , RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "admin@admin.com",
            };
            var user = await usermanger.FindByNameAsync(defaultUser.UserName);

            if (user == null)
            {
                await usermanger.CreateAsync(defaultUser, "P@ssword123");
                await usermanger.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                await roleManager.SeedClaimsForUser();
            }
        }

  
        private static async Task SeedClaimsForUser(this RoleManager<IdentityRole> roleManager)
        {
            var superadminRole = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
            await roleManager.AddPermissenstocalim(superadminRole, new List<string> {
                Modules.Employee.ToString(),
                Modules.GenralSettings.ToString(),
                Modules.Attendance.ToString(),
                Modules.AddNewGroup.ToString(),
                Modules.Premissions.ToString(),
                Modules.Users.ToString(),
                Modules.Salaryreport.ToString()
            });

        }


        public static async Task AddPermissenstocalim(this RoleManager<IdentityRole> roleManager , IdentityRole role , List<string> module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            foreach (var onemodule in module) { 
            var allPermissions = Permissions.GeneratePermissionsList(onemodule);

            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(c => c.Type == "Permission" && c.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("permission", permission));
                }
            }
            }
        }



    }
}

