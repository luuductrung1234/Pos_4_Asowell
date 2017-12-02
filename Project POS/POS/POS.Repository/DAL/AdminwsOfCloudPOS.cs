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
    public class AdminwsOfCloudPOS : IDisposable
    {

        private CloudContext _cloudContext;
        private GenericRepository<ApplicationLog> _appLogRepository;
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


        public AdminwsOfCloudPOS()
        {
            _cloudContext = new CloudContext();
        }

        public AdminwsOfCloudPOS(string connectionString)
        {
            _cloudContext = new CloudContext(connectionString);
        }



        public GenericRepository<ApplicationLog> AppLogRepository
        {
            get
            {
                if (_appLogRepository == null)
                {
                    _appLogRepository = new GenericRepository<ApplicationLog>(_cloudContext);
                }
                return _appLogRepository;
            }
        }

        public GenericRepository<WareHouse> WareHouseRepository
        {
            get
            {
                if (_wareHouseRepository == null)
                {
                    _wareHouseRepository = new GenericRepository<WareHouse>(_cloudContext);
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
                    _receiptnotedetailsRepository = new GenericRepository<ReceiptNoteDetail>(_cloudContext);
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
                    _receiptnoteRepository = new GenericRepository<ReceiptNote>(_cloudContext);
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
                    _ordernotedetailsRepository = new GenericRepository<OrderNoteDetail>(_cloudContext);
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
                    _productdetailsRepository = new GenericRepository<ProductDetail>(_cloudContext);
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
                    _adminreRepository = new GenericRepository<AdminRe>(_cloudContext);
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
                    _customerRepository = new GenericRepository<Customer>(_cloudContext);
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
                    _employeeRepository = new GenericRepository<Employee>(_cloudContext);
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
                    _ingredientRepository = new GenericRepository<Ingredient>(_cloudContext);
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
                    _productRepository = new GenericRepository<Product>(_cloudContext);
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
                    _orderRepository = new GenericRepository<OrderNote>(_cloudContext);
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
                    _salarynoteRepository = new GenericRepository<SalaryNote>(_cloudContext);
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
                    _workinghistoryRepository = new GenericRepository<WorkingHistory>(_cloudContext);
                }
                return _workinghistoryRepository;
            }
        }

        public void Save()
        {
            _cloudContext.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _cloudContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Refresh()
        {
            this.Save();
            var context = this._cloudContext;
            this._cloudContext = new CloudContext();
            context.Dispose();
        }
    }
}
