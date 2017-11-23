using System;
using POS.Context;
using POS.Entities;
using POS.Repository.Generic;

namespace POS.Repository.DAL
{
    /// <summary>
    /// The Employee WorkSpace Of Asowell (is a Unit Of Work class) that serves one purpose: to make sure that when you use multiple repositories that related each other in Employee WorkSpace situation, and they share a single database context. 
    /// That way, when a Employee WorkSpace Of Asowell is complete you can call the SaveChanges method on that instance of the context and be assured that all related changes will be coordinated.
    /// All that the class needs is a Save method and a property for each repository. 
    /// Each repository property returns a repository instance that has been instantiated using the same database context instance as the other repository instances.
    /// </summary>
    public class EmployeewsOfAsowell : IDisposable
    {
        private AsowellContext context = new AsowellContext();
        private GenericRepository<AdminRe> _adminreRepository;
        private GenericRepository<Customer> _customerRepository;
        private GenericRepository<Employee> _employeeRepository;
        private GenericRepository<SalaryNote> _salarynoteRepository;
        private GenericRepository<WorkingHistory> _workinghistoryRepository;
        private GenericRepository<Ingredient> _ingredientRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<OrderNote> _orderRepository;
        private GenericRepository<OrderNoteDetail> _orderDetailsRepository;
        private GenericRepository<ReceiptNote> _receiptNoteRepository;

        // business repo
        private GenericRepository<OrderDetailsTemp> _orderDetailsTempRepository;
        private GenericRepository<OrderTemp> _orderTempRepository;
        private GenericRepository<Chair> _chairRepository;
        private GenericRepository<Table> _tableRepository;

        public EmployeewsOfAsowell()
        {
            context = new AsowellContext();
        }

        public EmployeewsOfAsowell(string initalCatalog, string dataSource, string userId, string password)
        {
            context = new AsowellContext();
            context.ChangeDatabase(initalCatalog, dataSource, userId, password);
        }





        public GenericRepository<ReceiptNote> ReceiptNoteRepository
        {
            get
            {
                if (_receiptNoteRepository == null)
                {
                    _receiptNoteRepository = new GenericRepository<ReceiptNote>(context);
                }
                return _receiptNoteRepository;
            }
        }

        public GenericRepository<OrderNoteDetail> OrderDetailsRepository
        {
            get
            {
                if (_orderDetailsRepository == null)
                {
                    _orderDetailsRepository = new GenericRepository<OrderNoteDetail>(context);
                }
                return _orderDetailsRepository;
            }
        }

        public GenericRepository<Table> TableRepository
        {
            get
            {
                if (_tableRepository == null)
                {
                    _tableRepository = new GenericRepository<Table>(context);
                }
                return _tableRepository;
            }
        }

        public GenericRepository<Chair> ChairRepository
        {
            get
            {
                if (_chairRepository == null)
                {
                    _chairRepository = new GenericRepository<Chair>(context);
                }
                return _chairRepository;
            }
        }

        public GenericRepository<OrderTemp> OrderTempRepository
        {
            get
            {
                if (_orderTempRepository == null)
                {
                    _orderTempRepository = new GenericRepository<OrderTemp>(context);
                }
                return _orderTempRepository;
            }
        }

        public GenericRepository<OrderDetailsTemp> OrderDetailsTempRepository
        {
            get
            {
                if (_orderDetailsTempRepository == null)
                {
                    _orderDetailsTempRepository = new GenericRepository<OrderDetailsTemp>(context);
                }
                return _orderDetailsTempRepository;
            }
        }

        public GenericRepository<AdminRe> AdminreRepository
        {
            get
            {
                if (_adminreRepository == null)
                {
                    _adminreRepository = new GenericRepository<AdminRe>(context);
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
                    _customerRepository = new GenericRepository<Customer>(context);
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
                    _employeeRepository = new GenericRepository<Employee>(context);
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
                    _ingredientRepository = new GenericRepository<Ingredient>(context);
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
                    _productRepository = new GenericRepository<Product>(context);
                }
                return _productRepository;
            }
        }

        public GenericRepository<OrderNote> OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new GenericRepository<OrderNote>(context);
                }
                return _orderRepository;
            }
        }

        public GenericRepository<SalaryNote> SalaryNoteRepository
        {
            get
            {
                if (_salarynoteRepository == null)
                {
                    _salarynoteRepository = new GenericRepository<SalaryNote>(context);
                }
                return _salarynoteRepository;
            }
        }

        public GenericRepository<WorkingHistory> WorkingHistoryRepository
        {
            get
            {
                if (_workinghistoryRepository == null)
                {
                    _workinghistoryRepository = new GenericRepository<WorkingHistory>(context);
                }
                return _workinghistoryRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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
