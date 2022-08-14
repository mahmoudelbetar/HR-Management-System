using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HR_SYSTEM_V1.Models
{
    public class ApplicationUser:IdentityUser
    {
        public byte[]? profilePicture { get; set; }

    }
}
