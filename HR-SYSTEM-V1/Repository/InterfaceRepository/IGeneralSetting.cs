using HR_SYSTEM_V1.Models;
namespace HR_SYSTEM_V1.Repository.InterfaceRepository
{
    public interface IGeneralSetting
    {
        public bool checkGeneralSetting();
        public void addDiscountExtra( DiscountExtra discountExtra);
        public void addHolidays(Holiday holiday);

        public Holiday getLastHoliday();
        public DiscountExtra getLastExtraDiscount();
    }
}
