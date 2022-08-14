using HR_SYSTEM_V1.Data;
using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.Repository.InterfaceRepository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HR_SYSTEM_V1.Repository.ClassRepository
{
    public class AttendanceRepository:IAttendance
    {
       private readonly ApplicationDbContext _db;

        public AttendanceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Attendance> getAll()
        {
            return _db.Attendances.Include(n => n.employee).ToList();
        }

        public void add(Attendance attendance)
        {
            _db.Attendances.Add(attendance);
            _db.SaveChanges();
        }



        public void Update(int id , Attendance newAttendance)
        {
            Attendance oldAttendace = _db.Attendances.FirstOrDefault(x=>x.Attend_Id==id);   

            oldAttendace.ExtraTime = newAttendance.ExtraTime;
            oldAttendace.DiscountTime = newAttendance.DiscountTime;
            oldAttendace.Absent = false;
            oldAttendace.Day_Date = newAttendance.Day_Date;
            oldAttendace.StartTimeWork = newAttendance.StartTimeWork;
            oldAttendace.EndTimeWork = newAttendance.EndTimeWork;
            oldAttendace.Emp_Id = newAttendance.Emp_Id;

            _db.SaveChanges();

        }



        public void Delete(Attendance attendance )
        {
            _db.Remove(attendance);
            _db.SaveChanges();
        }

        public Attendance GetById(int id)
        {
            return _db.Attendances.Include(x=>x.employee).FirstOrDefault(a => a.Attend_Id == id);
        }






    }
}
