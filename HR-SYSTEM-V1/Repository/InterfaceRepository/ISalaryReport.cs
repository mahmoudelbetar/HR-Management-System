using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.ViewModel;

namespace HR_SYSTEM_V1.Repository.InterfaceRepository
{
    public interface ISalaryReport
    {

        public List<Salary> getAll();
        public List<SalaryReportViewModel> CalcSalary();
        public void AddSalary();

    }
}
