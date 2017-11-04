using System;
using System.Collections.Generic;
using POS.Entities;

namespace POS.Repository.Interfaces
{
    public interface IAdminRepository : IDisposable
    {
        IEnumerable<AdminRe> GetAllAdminRes();
        AdminRe GetAdminReById(string adminReId);
        void InsertAdminRe(AdminRe adminRe);
        void DeleteAdminRe(string adminReId);
        void UpdateAdminRe(AdminRe adminRe);
        void Save();
    }
}
