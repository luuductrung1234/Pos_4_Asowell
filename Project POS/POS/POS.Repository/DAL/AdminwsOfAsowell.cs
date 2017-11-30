using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Context;
using POS.Entities;
using POS.Repository.Generic;

namespace POS.Repository.DAL
{
    public class AdminwsOfAsowell : IDisposable
    {
        private AsowellContext localContext;

        private GenericRepository<AdminRe> _adminreRepository;
        private GenericRepository<Customer> _customerRepository;
        private GenericRepository<Employee> _employeeRepository;
        private GenericRepository<Ingredient> _ingredientRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<WareHouse> _wareHouseRepository;

        public AdminwsOfAsowell()
        {
            localContext = new AsowellContext();
        }

        public AdminwsOfAsowell(string connectionString)
        {
            localContext = new AsowellContext(connectionString);
        }



        public GenericRepository<WareHouse> WareHouseRepository
        {
            get
            {
                if (_wareHouseRepository == null)
                {
                    _wareHouseRepository = new GenericRepository<WareHouse>(localContext);
                }
                return _wareHouseRepository;
            }
        }

        public GenericRepository<AdminRe> AdminreRepository
        {
            get
            {
                if (_adminreRepository == null)
                {
                    _adminreRepository = new GenericRepository<AdminRe>(localContext);
                }
                return _adminreRepository;
            }
        }

        public GenericRepository<Customer> CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new GenericRepository<Customer>(localContext);
                }
                return _customerRepository;
            }
        }

        public GenericRepository<Employee> EmployeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                {
                    _employeeRepository = new GenericRepository<Employee>(localContext);
                }
                return _employeeRepository;
            }
        }

        public GenericRepository<Ingredient> IngredientRepository
        {
            get
            {
                if (_ingredientRepository == null)
                {
                    _ingredientRepository = new GenericRepository<Ingredient>(localContext);
                }
                return _ingredientRepository;
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new GenericRepository<Product>(localContext);
                }
                return _productRepository;
            }
        }


        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    localContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
