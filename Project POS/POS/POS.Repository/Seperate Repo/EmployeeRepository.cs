using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POS.Context;
using POS.Entities;
using POS.Repository.Interfaces;

namespace POS.Repository.SeperateRepo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private CloudContext context;

        public EmployeeRepository(CloudContext context)
        {
            this.context = context;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employees.ToList();
        }

        public Employee GetEmployeeById(string employeeId)
        {
            return context.Employees.Find(employeeId);
        }

        public void InsertEmployee(Employee employee)
        {
            context.Employees.Add(employee);
        }

        public void DeleteEmployee(string employeeId)
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