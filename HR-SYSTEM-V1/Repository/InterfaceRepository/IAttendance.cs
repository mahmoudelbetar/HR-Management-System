using HR_SYSTEM_V1.Models;

namespace HR_SYSTEM_V1.Repository.InterfaceRepository
{
    public interface IAttendance
    {
        public List<Attendance> getAll();
        public void add(Attendance attendance);

        public void Update(int id, Attendance newAttendance);
        public void Delete(Attendance attendance);
        public Attendance GetById(int id);
        
    }
}
