using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POS.Context;
using POS.Entities;
using POS.Repository.Interfaces;

namespace POS.Repository.SeperateRepo
{
    public class AdminReRepository : IAdminRepository
    {
        private CloudContext context;

        public AdminReRepository(CloudContext context)
        {
            this.context = context;
        }

        public IEnumerable<AdminRe> GetAllAdminRes()
        {
            return context.AdminRes.ToList();
        }

        public AdminRe GetAdminReById(string adminreId)
        {
            return context.AdminRes.Find(adminreId);
        }

        public void InsertAdminRe(AdminRe adminre)
        {
            context.AdminRes.Add(adminre);
        }

        public void DeleteAdminRe(string adminreId)
        {
            AdminRe adminre = context.AdminRes.Find(adminreId);
            context.AdminRes.Remove(adminre);
        }

        public void UpdateAdminRe(AdminRe adminre)
        {
            context.Entry(adminre).State = EntityState.Modified;
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
