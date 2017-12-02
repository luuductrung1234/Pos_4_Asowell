using System;
using POS.BusinessContext;
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
    public class EmployeewsOfLocalPOS : IDisposable
    {
        private LocalContext context;

        // business repo
        private GenericRepositoryForLocal<ApplicationLog> _appLogRepository;
        private GenericRepositoryForLocal<OrderDetailsTemp> _orderDetailsTempRepository;
        private GenericRepositoryForLocal<OrderTemp> _orderTempRepository;
        private GenericRepositoryForLocal<Chair> _chairRepository;
        private GenericRepositoryForLocal<Table> _tableRepository;

        public EmployeewsOfLocalPOS()
        {
            context = new LocalContext();
        }

        public EmployeewsOfLocalPOS(string connectionString)
        {
            context = new LocalContext(connectionString);
        }



        public GenericRepositoryForLocal<ApplicationLog> AppLogRepository
        {
            get
            {
                if (_appLogRepository == null)
                {
                    _appLogRepository = new GenericRepositoryForLocal<ApplicationLog>(context);
                }
                return _appLogRepository;
            }
        }

        public GenericRepositoryForLocal<Table> TableRepository
        {
            get
            {
                if (_tableRepository == null)
                {
                    _tableRepository = new GenericRepositoryForLocal<Table>(context);
                }
                return _tableRepository;
            }
        }

        public GenericRepositoryForLocal<Chair> ChairRepository
        {
            get
            {
                if (_chairRepository == null)
                {
                    _chairRepository = new GenericRepositoryForLocal<Chair>(context);
                }
                return _chairRepository;
            }
        }

        public GenericRepositoryForLocal<OrderTemp> OrderTempRepository
        {
            get
            {
                if (_orderTempRepository == null)
                {
                    _orderTempRepository = new GenericRepositoryForLocal<OrderTemp>(context);
                }
                return _orderTempRepository;
            }
        }

        public GenericRepositoryForLocal<OrderDetailsTemp> OrderDetailsTempRepository
        {
            get
            {
                if (_orderDetailsTempRepository == null)
                {
                    _orderDetailsTempRepository = new GenericRepositoryForLocal<OrderDetailsTemp>(context);
                }
                return _orderDetailsTempRepository;
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
