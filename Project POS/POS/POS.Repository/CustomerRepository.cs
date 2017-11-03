using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Context;
using POS.Entities;
using POS.Repository.Interfaces;

namespace POS.Repository
{
    public class CustomerRepository : Interfaces.ICustomerRepository
    {
        private AsowellContext context;

        public CustomerRepository(AsowellContext context)
        {
            this.context = context;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return context.Customers.ToList();
        }

        public Customer GetCustomerById(int customerId)
        {
            return context.Customers.Find(customerId);
        }

        public void InsertCustomer(Customer customer)
        {
            context.Customers.Add(customer);
        }

        public void DeleteCustomer(int customerId)
        {
            Customer customer = context.Customers.Find(customerId);
            context.Customers.Remove(customer ?? throw new InvalidOperationException());
        }

        public void UpdateCustomer(Customer customer)
        {
            context.Entry(customer).State = EntityState.Modified;
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
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
