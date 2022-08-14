using Microsoft.AspNetCore.Authorization;

namespace HR_SYSTEM_V1.Filters
{
    public class PermissionRequirement: IAuthorizationRequirement
    {
        public string Permission { get; private set; }
        public PermissionRequirement(string permission)
        {
            Permission = permission;

        }


    }
}
