using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.Data;
using HR_SYSTEM_V1.Repository.InterfaceRepository;

namespace HR_SYSTEM_V1.Repository.ClassRepository
{
    public class GeneralSettingRepository : IGeneralSetting
    {
        ApplicationDbContext db;
        public GeneralSettingRepository(ApplicationDbContext database)
        {
            this.db = database;
        }

        public bool checkGeneralSetting()
        {
            if (db.DiscountExtras.ToList().Count() == 0)
            {
                return true;
            }
            else return false;
        }
        public DiscountExtra getLastExtraDiscount ()
        {
            
            DiscountExtra? DX = db.DiscountExtras.OrderBy(x=>x.CreatedDate).LastOrDefault();
            if (DX == null)
            {
                return new DiscountExtra();
            }
            return DX;
        }

        public Holiday getLastHoliday()
        {
            Holiday? H = db.Holidays.OrderBy(x => x.Edit_Day_General).LastOrDefault();
            if ( H == null )
            {
                return new Holiday();
            }
            return H;
        }

        public void addDiscountExtra(DiscountExtra discountExtra)
        {
            db.DiscountExtras.Add(discountExtra);
            db.SaveChanges();
        }

        public void addHolidays(Holiday holiday)
        {
            db.Holidays.Add(holiday);   
            db.SaveChanges();
        }
    }
}
