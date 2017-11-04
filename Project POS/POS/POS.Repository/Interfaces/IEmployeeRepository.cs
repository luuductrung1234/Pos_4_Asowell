using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Entities;

namespace POS.Repository.Interfaces
{
    public interface IEmployeeRepository : IDisposable
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(string employeeId);
        void InsertEmployee(Employee employee);
        void DeleteEmployee(string employeeId);
        void UpdateEmployee(Employee employee);
        void Save();
    }
}
