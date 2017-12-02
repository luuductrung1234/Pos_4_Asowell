using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POS.Context;
using POS.Entities;

namespace POS.Repository.SeperateRepo
{
    public class CustomerRepository : Interfaces.ICustomerRepository
    {
        private CloudContext context;

        public CustomerRepository(CloudContext context)
        {
            this.context = context;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return context.Customers.ToList();
        }

        public Customer GetCustomerById(string customerId)
        {
            return context.Customers.Find(customerId);
        }

        public void InsertCustomer(Customer customer)
        {
            context.Customers.Add(customer);
        }

        public void DeleteCustomer(string customerId)
        {
            Customer customer = context.Customers.Find(customerId);
            context.Customers.Remove(customer);
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
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
