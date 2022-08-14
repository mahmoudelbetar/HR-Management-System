using HR_SYSTEM_V1.Validation;
using System.ComponentModel.DataAnnotations;

namespace HR_SYSTEM_V1.Models
{
    public class DiscountExtra
    {
        [Key]
        public int Disc_Id { get; set; }
        [ExtraDiscount]
        public decimal Discount { get; set; }
        [ExtraDiscount]
        public decimal Extra { get; set; }

        public ExtraDiscountType Type { get; set; }

        public DateTime CreatedDate { get; set; } =DateTime.Now;

    }

    public enum ExtraDiscountType
    {
        Hours ,
        Money,
    }
}
