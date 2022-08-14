using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.Validation;
using System.ComponentModel.DataAnnotations;

namespace HR_SYSTEM_V1.ViewModel
{
    public class GeneralSettingViewModel
    {
        [Required]
        [ExtraDiscount]
        public decimal Discount { get; set; }
        [Required]
        [ExtraDiscount]
        public decimal Extra { get; set; }

        public ExtraDiscountType ExtraDiscountType { get; set; }

        public bool Saturday { get; set; } = false;
        public bool Sunday { get; set; } = false;
        public bool Monday { get; set; } = false;
        public bool Tuesday { get; set; } = false;
        public bool Wednesday { get; set; } = false;
        public bool Thursday { get; set; } = false;
        public bool Friday { get; set; } = false;
    }

    //public enum ExtraDiscountType
    //{
    //    Hours , 
    //    Money ,
    //}
}
