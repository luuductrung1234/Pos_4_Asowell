using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using POS.Context;
using POS.Entities;
using POS.Repository.Interfaces;

namespace POS.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private AsowellContext context;

        public EmployeeRepository(AsowellContext context)
        {
            this.context = context;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employees.ToList();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return context.Employees.Find(employeeId);
        }

        public void InsertEmployee(Employee employee)
        {
            context.Employees.Add(employee);
        }

        public void DeleteEmployee(int employeeId)
        {
            Employee employee = context.Employees.Find(employeeId);
            context.Employees.Remove(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}