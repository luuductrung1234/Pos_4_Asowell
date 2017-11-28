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
        private AsowellContext context;

        // business repo
        private GenericRepository<OrderDetailsTemp> _orderDetailsTempRepository;
        private GenericRepository<OrderTemp> _orderTempRepository;
        private GenericRepository<Chair> _chairRepository;
        private GenericRepository<Table> _tableRepository;

        public EmployeewsOfAsowell()
        {
            context = new AsowellContext();
        }

        public EmployeewsOfAsowell(string connectionString)
        {
            context = new AsowellContext(connectionString);
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
