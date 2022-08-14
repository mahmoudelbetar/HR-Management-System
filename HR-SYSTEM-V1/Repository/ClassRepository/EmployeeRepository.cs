using HR_SYSTEM_V1.Data;
using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.Repository.InterfaceRepository;
using Microsoft.EntityFrameworkCore;

namespace HR_SYSTEM_V1.Repository.ClassRepository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        //-------------------- Get ------------------------------
        public List<Employee> getAll()
        {
            return _db.Employees.Include(x=>x.attendances).ToList();
        }

        //-------------------- Get By Id ------------------------------

        public Employee getByID(int id)
        {
            return _db.Employees.FirstOrDefault(x=>x.Emp_Id == id); 
        }

        //-------------------- Add ------------------------------

        public void Add(Employee emp)
        {
            _db.Employees.Add(emp);
            _db.SaveChanges();
        }


        //-------------------- Update -----------------------


        public void updateEmployee(Employee emp)
        {
            _db.Employees.Update(emp);
            _db.SaveChanges();
        }


        //------------------------- Delete ------------------------


        public void delete(int id)
        {
            Employee emp = _db.Employees.FirstOrDefault(n => n.Emp_Id == id);

            _db.Employees.Remove(emp);
            _db.SaveChanges();
        }


        public bool Check_National_Id(int id ,string national_id)
        {
            return _db.Employees.Any(b => b.national_Id == national_id && b.Emp_Id != id);    
        }



        //public bool IsDuplicateIsbn(int bookId, string bookISBN)
        //{
        //    var Book = _bookContext.Books.Where(b => b.Isbn.Trim().ToUpper() == bookISBN.Trim().ToUpper()
        //                                        && b.Id != bookId).FirstOrDefault();
        //    if (Book == null) return false;
        //    return true;

        //}

        //public Employee Check_National_Id(string national_id)
        //{
        //    return _db.Employees.Where(b => b.national_Id == national_id).FirstOrDefault();
        //}


    }
}
