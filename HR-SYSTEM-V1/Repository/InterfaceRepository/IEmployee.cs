using HR_SYSTEM_V1.Models;
namespace HR_SYSTEM_V1.Repository.InterfaceRepository
{
    public interface IEmployee
    {
        public List<Employee> getAll();
        public Employee getByID ( int id);
        public void Add(Employee emp);
        public void updateEmployee (Employee employee);
        public void delete(int id);
        public bool Check_National_Id(int id ,string national_id);
        //public Employee Check_National_Id(string national_id);

    }
}
