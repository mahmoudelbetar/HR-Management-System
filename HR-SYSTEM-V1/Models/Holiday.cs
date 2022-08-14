using System.ComponentModel.DataAnnotations;

namespace HR_SYSTEM_V1.Models
{
    public class Holiday
    {
        [Key]
        public int Holiday_Id { get; set; }
        public DateTime Edit_Day_General { get; set; } = DateTime.Now;
        public bool Saturday { get; set; } = false;
        public bool Sunday { get; set; } = false;
        public bool Monday { get; set; } = false ;
        public bool Tuesday { get; set; } = false;
        public bool Wednesday { get; set; } = false;
        public bool Thursday { get; set; } = false;
        public bool Friday { get; set; } = false;



    }
}
