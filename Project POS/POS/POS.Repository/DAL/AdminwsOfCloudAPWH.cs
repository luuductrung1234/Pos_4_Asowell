using POS.Context;
using POS.Entities;
using POS.Repository.Generic;
using System;

namespace POS.Repository.DAL
{
    public class AdminwsOfCloudAPWH : IDisposable
    {
        private CloudContext _cloudContext;
        private GenericRepository<AdminRe> _adminRepository;
        private GenericRepository<APWareHouse> _apwareHouseRepository;
        private GenericRepository<Stock> _stockRepository;
        private GenericRepository<StockIn> _stockInRepository;
        private GenericRepository<StockInDetails> _stockInDetailsRepository;
        private GenericRepository<StockOut> _stockOutRepository;
        private GenericRepository<StockOutDetails> _stockOutDetailsRepository;



        public AdminwsOfCloudAPWH()
        {
            _cloudContext = new CloudContext();
        }

        public AdminwsOfCloudAPWH(string connectionString)
        {
            _cloudContext = new CloudContext(connectionString);
        }


        public GenericRepository<AdminRe> AdminreRepository
        {
            get
            {
                if (_adminRepository == null)
                {
                    _adminRepository = new GenericRepository<AdminRe>(_cloudContext);
                }
                return _adminRepository;
            }
        }
        public GenericRepository<APWareHouse> APWareHouseRepository
        {
            get
            {
                if (_apwareHouseRepository == null)
                {
                    _apwareHouseRepository = new GenericRepository<APWareHouse>(_cloudContext);
                }
                return _apwareHouseRepository;
            }
        }
        public GenericRepository<Stock> StockRepository
        {
            get
            {
                if (_stockRepository == null)
                {
                    _stockRepository = new GenericRepository<Stock>(_cloudContext);
                }
                return _stockRepository;
            }
        }
        public GenericRepository<StockIn> StockInRepository
        {
            get
            {
                if (_stockInRepository == null)
                {
                    _stockInRepository = new GenericRepository<StockIn>(_cloudContext);
                }
                return _stockInRepository;
            }
        }
        public GenericRepository<StockInDetails> StockInDetailsRepository
        {
            get
            {
                if (_stockInDetailsRepository == null)
                {
                    _stockInDetailsRepository = new GenericRepository<StockInDetails>(_cloudContext);
                }
                return _stockInDetailsRepository;
            }
        }
        public GenericRepository<StockOut> StockOutRepository
        {
            get
            {
                if (_stockOutRepository == null)
                {
                    _stockOutRepository = new GenericRepository<StockOut>(_cloudContext);
                }
                return _stockOutRepository;
            }
        }
        public GenericRepository<StockOutDetails> StockOutDetailsRepository
        {
            get
            {
                if (_stockOutDetailsRepository == null)
                {
                    _stockOutDetailsRepository = new GenericRepository<StockOutDetails>(_cloudContext);
                }
                return _stockOutDetailsRepository;
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
            this.Dispose();
            this._cloudContext = new CloudContext();
        }
    }
}
