using POS.Context;
using POS.Entities;
using POS.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Repository.DAL
{
    public class AdminwsOfAsowell : IDisposable
    {
        private AsowellContext context = new AsowellContext();
        private GenericRepository<AdminRe> _adminreRepository;
        private GenericRepository<Customer> _customerRepository;
        private GenericRepository<Employee> _employeeRepository;
        private GenericRepository<SalaryNote> _salarynoteRepository;
        private GenericRepository<WorkingHistory> _workinghistoryRepository;
        private GenericRepository<Ingredient> _ingredientRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<ProductDetail> _productdetailsRepository;
        private GenericRepository<OrderNote> _orderRepository;
        private GenericRepository<OrderNoteDetail> _ordernotedetailsRepository;
        private GenericRepository<ReceiptNote> _receiptnoteRepository;
        private GenericRepository<ReceiptNoteDetail> _receiptnotedetailsRepository;
        private GenericRepository<WareHouse> _wareHouseRepository;


        public AdminwsOfAsowell()
        {
            context = new AsowellContext();
        }

        public AdminwsOfAsowell(string initalCatalog, string dataSource, string userId, string password)
        {
            context = new AsowellContext();
            context.ChangeDatabase(initalCatalog, dataSource, userId, password);
        }





        public GenericRepository<WareHouse> WareHouseRepository
        {
            get
            {
                if (_wareHouseRepository == null)
                {
                    _wareHouseRepository = new GenericRepository<WareHouse>(context);
                }
                return _wareHouseRepository;
            }
        }

        public GenericRepository<ReceiptNoteDetail> ReceiptNoteDsetailsRepository
        {
            get
            {
                if (_receiptnotedetailsRepository == null)
                {
                    _receiptnotedetailsRepository = new GenericRepository<ReceiptNoteDetail>(context);
                }
                return _receiptnotedetailsRepository;
            }
        }

        public GenericRepository<ReceiptNote> ReceiptNoteRepository
        {
            get
            {
                if (_receiptnoteRepository == null)
                {
                    _receiptnoteRepository = new GenericRepository<ReceiptNote>(context);
                }
                return _receiptnoteRepository;
            }
        }

        public GenericRepository<OrderNoteDetail> OrderNoteDetailsRepository
        {
            get
            {
                if (_ordernotedetailsRepository == null)
                {
                    _ordernotedetailsRepository = new GenericRepository<OrderNoteDetail>(context);
                }
                return _ordernotedetailsRepository;
            }
        }

        public GenericRepository<ProductDetail> ProductDetailsRepository
        {
            get
            {
                if (_productdetailsRepository == null)
                {
                    _productdetailsRepository = new GenericRepository<ProductDetail>(context);
                }
                return _productdetailsRepository;
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
