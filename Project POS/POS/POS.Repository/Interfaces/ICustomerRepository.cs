using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Entities;

namespace POS.Repository.Interfaces
{
    public interface ICustomerRepository : IDisposable
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int customerId);
        void InsertCustomer(Customer customer);
        void DeleteCustomer(int customerId);
        void UpdateCustomer(Customer customer);
        void Save();
    }
}
