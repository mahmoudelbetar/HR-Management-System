using HR_SYSTEM_V1.Constants;
using Microsoft.AspNetCore.Identity;

namespace HR_SYSTEM_V1.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any()) { 
                
                await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            
            }
        }

    }
}
