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
    public class AdminwsOfCloud : IDisposable
    {
        private string cloudConnectString =
            "Server=tcp:commasv.database.windows.net,1433;" +
            "Initial Catalog=DBAsowell;" +
            "Persist Security Info=False;" +
            "User ID=sampleuser;Password=Trung1997;" +
            "MultipleActiveResultSets=False;Encrypt=True;" +
            "TrustServerCertificate=False;Connection Timeout=30;";

        private AsowellContext cloudContext;    

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


        public AdminwsOfCloud()
        {
            cloudContext = new AsowellContext(cloudConnectString);
        }

        public AdminwsOfCloud(string connectionString)
        {
            cloudConnectString = connectionString;
            cloudContext = new AsowellContext(cloudConnectString);
        }





        public GenericRepository<WareHouse> WareHouseRepository
        {
            get
            {
                if (_wareHouseRepository == null)
                {
                    _wareHouseRepository = new GenericRepository<WareHouse>(cloudContext);
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
                    _receiptnotedetailsRepository = new GenericRepository<ReceiptNoteDetail>(cloudContext);
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
                    _receiptnoteRepository = new GenericRepository<ReceiptNote>(cloudContext);
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
                    _ordernotedetailsRepository = new GenericRepository<OrderNoteDetail>(cloudContext);
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
                    _productdetailsRepository = new GenericRepository<ProductDetail>(cloudContext);
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
                    _adminreRepository = new GenericRepository<AdminRe>(cloudContext);
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
                    _customerRepository = new GenericRepository<Customer>(cloudContext);
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
                    _employeeRepository = new GenericRepository<Employee>(cloudContext);
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
                    _ingredientRepository = new GenericRepository<Ingredient>(cloudContext);
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
                    _productRepository = new GenericRepository<Product>(cloudContext);
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
                    _orderRepository = new GenericRepository<OrderNote>(cloudContext);
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
                    _salarynoteRepository = new GenericRepository<SalaryNote>(cloudContext);
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
                    _workinghistoryRepository = new GenericRepository<WorkingHistory>(cloudContext);
                }
                return _workinghistoryRepository;
            }
        }

        public void Save()
        {
            cloudContext.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    cloudContext.Dispose();
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
            this.Dispose();
            this.cloudContext = new AsowellContext(cloudConnectString);
        }
    }
}
