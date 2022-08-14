using System.ComponentModel.DataAnnotations;

namespace HR_SYSTEM_V1.ViewModel
{
    public class RoleFormViewModel
    {
        [Required, StringLength(256)]
        public string Name { get; set; }
    }
}
