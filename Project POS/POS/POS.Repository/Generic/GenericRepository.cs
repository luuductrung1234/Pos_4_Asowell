using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using POS.Context;
using POS.Entities;
using POS.Repository.Security;

namespace POS.Repository.Generic
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal CloudContext context;
        internal DbSet dbSet;

        public GenericRepository(CloudContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();


            if (typeof(TEntity) == typeof(AdminRe))
            {
                foreach (var entity in dbSet)
                {
                    var admin = entity as AdminRe;
                    string decryptPass = AESThenHMAC.SimpleDecryptWithPassword(admin.Pass, "itcomma_luuductrung");
                    admin.DecryptedPass = decryptPass;
                }
            }
            else
            {
                if (typeof(TEntity) == typeof(Employee))
                {
                    foreach (var entity in dbSet)
                    {
                        var emp = entity as Employee;
                        string decryptPass = AESThenHMAC.SimpleDecryptWithPassword(emp.Pass, "itcomma_luuductrung");
                        string decryptCode = AESThenHMAC.SimpleDecryptWithPassword(emp.EmpCode, "itcomma_luuductrung");
                        emp.DecryptedPass = decryptPass;
                        emp.DecryptedCode = decryptCode;
                    }
                }
            }
        }


        /// <summary>
        /// Get data
        /// </summary>
        /// <param name="filter">Lambda expression to filtering data</param>
        /// <param name="orderBy">Lambda expression to ordering data</param>
        /// <param name="includeProperties">the properties represent the relationship with other entities (use ',' to seperate these properties)</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                // Apply the filter expression
                IQueryable<TEntity> query = (IQueryable<TEntity>) dbSet;


                if (filter != null)
                {
                    query = query.Where(filter);
                }


                // Loading related data (using eager-loading)
                foreach (var includeProperty in includeProperties.Split(
                    new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                // Apply the orderBy expression
                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }



                return query.ToList();
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                return Get(filter, orderBy, includeProperties);
            }
        }

        public virtual TEntity GetById(object id)
        {
            try
            {
                return (TEntity) dbSet.Find(id);
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                return GetById(id);
            }
        }

        public virtual void Insert(TEntity entity)
        {
            try
            {
                if (typeof(TEntity) == typeof(AdminRe))
                {
                    var admin = entity as AdminRe;
                    string encryptPass = AESThenHMAC.SimpleEncryptWithPassword(admin.Pass, "itcomma_luuductrung");
                    admin.Pass = encryptPass;
                }
                else
                {
                    if (typeof(TEntity) == typeof(Employee))
                    {
                        var emp = entity as Employee;
                        string encryptPass = AESThenHMAC.SimpleEncryptWithPassword(emp.Pass, "itcomma_luuductrung");
                        string encryptCode = AESThenHMAC.SimpleEncryptWithPassword(emp.EmpCode, "itcomma_luuductrung");
                        emp.Pass = encryptPass;
                        emp.EmpCode = encryptCode;
                    }
                }


                dbSet.Add(AutoGeneteId_DBAsowell(entity));
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                Insert(entity);
            }
        }

        public virtual void Delete(object id)
        {
            try
            {
                TEntity entityToDelete = (TEntity) dbSet.Find(id);
                dbSet.Remove(entityToDelete);
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                Delete(id);
            }
        }

        public virtual void Delete(TEntity entityTODelete)
        {
            try
            {
                if (context.Entry(entityTODelete).State == EntityState.Deleted)
                {
                    dbSet.Attach(entityTODelete);
                }
                dbSet.Remove(entityTODelete);
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                Delete(entityTODelete);
            }
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            try
            {
                if (typeof(TEntity) == typeof(AdminRe))
                {
                    var admin = entityToUpdate as AdminRe;

                    //check the password change
                    try
                    {
                        string decryptPass = AESThenHMAC.SimpleDecryptWithPassword(admin.Pass, "itcomma_luuductrung");
                        if (decryptPass.Equals(admin.DecryptedPass))
                        {
                            admin.Pass = admin.DecryptedPass;
                        }
                    }
                    catch (Exception ex)
                    {
                        //occur when the admin.Pass is not a encrypt password
                    }

                    string encryptPass = AESThenHMAC.SimpleEncryptWithPassword(admin.Pass, "itcomma_luuductrung");
                    admin.Pass = encryptPass;
                }
                else
                {
                    if (typeof(TEntity) == typeof(Employee))
                    {
                        var emp = entityToUpdate as Employee;

                        //check the password change
                        try
                        {
                            string decryptPass = AESThenHMAC.SimpleDecryptWithPassword(emp.Pass, "itcomma_luuductrung");
                            string decryptCode =
                                AESThenHMAC.SimpleDecryptWithPassword(emp.EmpCode, "itcomma_luuductrung");
                            if (decryptPass.Equals(emp.DecryptedPass))
                            {
                                emp.Pass = emp.DecryptedPass;
                            }
                            if (decryptCode.Equals(emp.EmpCode))
                            {
                                emp.EmpCode = emp.DecryptedCode;
                            }
                        }
                        catch (Exception ex)
                        {
                            //occur when the emp.Pass is not a encrypt password
                        }


                        string encryptPass = AESThenHMAC.SimpleEncryptWithPassword(emp.Pass, "itcomma_luuductrung");
                        string encryptCode = AESThenHMAC.SimpleEncryptWithPassword(emp.EmpCode, "itcomma_luuductrung");
                        emp.Pass = encryptPass;
                        emp.EmpCode = encryptCode;
                    }
                }


                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                Update(entityToUpdate);
            }
        }

        private static int ID_SIZE_DBASOWELL = 10;
        /// <summary>
        /// auto generate id for all entities in Asowell Database
        /// all id type is 10 character and the sign is depend on the type of entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private TEntity AutoGeneteId_DBAsowell(TEntity entity)
        {
            string sign = "";
            if (entity is Employee)
            {
                sign = "EMP";
                // lấy số thứ tự mới nhất
                string numberWantToset = (this.Get().Count() + 1).ToString();

                int blank = ID_SIZE_DBASOWELL - (sign.Length + numberWantToset.Length);
                string result = sign;
                for (int i = 0; i < blank; i++)
                {
                    result += "0";
                }
                result += numberWantToset;

                Employee emp = entity as Employee;
                emp.EmpId = result;
            }
            else if (entity is AdminRe)
            {
                sign = "AD";
                // lấy số thứ tự mới nhất
                string numberWantToset = (this.Get().Count() + 1).ToString();

                int blank = ID_SIZE_DBASOWELL - (sign.Length + numberWantToset.Length);
                string result = sign;
                for (int i = 0; i < blank; i++)
                {
                    result += "0";
                }
                result += numberWantToset;


                AdminRe admin = entity as AdminRe;
                admin.AdId = result;
            }
            else if (entity is Customer)
            {
                sign = "CUS";
                // lấy số thứ tự mới nhất
                string numberWantToset = (this.Get().Count() + 1).ToString();

                int blank = ID_SIZE_DBASOWELL - (sign.Length + numberWantToset.Length);
                string result = sign;
                for (int i = 0; i < blank; i++)
                {
                    result += "0";
                }
                result += numberWantToset;

                Customer cus = entity as Customer;
                cus.CusId = result;
            }
            else if (entity is WareHouse)
            {
                sign = "WAH";
                // lấy số thứ tự mới nhất
                string numberWantToset = (this.Get().Count() + 1).ToString();

                int blank = ID_SIZE_DBASOWELL - (sign.Length + numberWantToset.Length);
                string result = sign;
                for (int i = 0; i < blank; i++)
                {
                    result += "0";
                }
                result += numberWantToset;

                WareHouse wh = entity as WareHouse;
                wh.WarehouseId = result;
            }
            else if (entity is Ingredient)
            {
                sign = "IGD";
                // lấy số thứ tự mới nhất
                string numberWantToset = (this.Get().Count() + 1).ToString();

                int blank = ID_SIZE_DBASOWELL - (sign.Length + numberWantToset.Length);
                string result = sign;
                for (int i = 0; i < blank; i++)
                {
                    result += "0";
                }
                result += numberWantToset;

                Ingredient ign = entity as Ingredient;
                ign.IgdId = result;
            }
            else if (entity is Product)
            {
                sign = "P";
                // lấy số thứ tự mới nhất
                string numberWantToset = (this.Get().Count() + 1).ToString();

                int blank = ID_SIZE_DBASOWELL - (sign.Length + numberWantToset.Length);
                string result = sign;
                for (int i = 0; i < blank; i++)
                {
                    result += "0";
                }
                result += numberWantToset;

                Product p = entity as Product;
                p.ProductId = result;
            }
            else if (entity is ProductDetail)
            {
                sign = "PD";
                // lấy số thứ tự mới nhất
                string numberWantToset = (this.Get().Count() + 1).ToString();

                int blank = ID_SIZE_DBASOWELL - (sign.Length + numberWantToset.Length);
                string result = sign;
                for (int i = 0; i < blank; i++)
                {
                    result += "0";
                }
                result += numberWantToset;

                ProductDetail pd = entity as ProductDetail;
                pd.PdetailId = result;
            }
            else if (entity is OrderNote)
            {
                sign = "ORD";
                // lấy số thứ tự mới nhất
                string numberWantToset = (this.Get().Count() + 1).ToString();

                int blank = ID_SIZE_DBASOWELL - (sign.Length + numberWantToset.Length);
                string result = sign;
                for (int i = 0; i < blank; i++)
                {
                    result += "0";
                }
                result += numberWantToset;

                OrderNote ord = entity as OrderNote;
                ord.OrdernoteId = result;
            }
            else if (entity is ReceiptNote)
            {
                sign = "RN";
                // lấy số thứ tự mới nhất
                string numberWantToset = (this.Get().Count() + 1).ToString();

                int blank = ID_SIZE_DBASOWELL - (sign.Length + numberWantToset.Length);
                string result = sign;
                for (int i = 0; i < blank; i++)
                {
                    result += "0";
                }
                result += numberWantToset;

                ReceiptNote rcn = entity as ReceiptNote;
                rcn.RnId = result;
            }
            else if (entity is SalaryNote)
            {
                sign = "SAN";
                // lấy số thứ tự mới nhất
                string numberWantToset = (this.Get().Count() + 1).ToString();

                int blank = ID_SIZE_DBASOWELL - (sign.Length + numberWantToset.Length);
                string result = sign;
                for (int i = 0; i < blank; i++)
                {
                    result += "0";
                }
                result += numberWantToset;

                SalaryNote sln = entity as SalaryNote;
                sln.SnId = result;
            }
            else if (entity is WorkingHistory)
            {
                sign = "WOH";
                // lấy số thứ tự mới nhất
                string numberWantToset = (this.Get().Count() + 1).ToString();

                int blank = ID_SIZE_DBASOWELL - (sign.Length + numberWantToset.Length);
                string result = sign;
                for (int i = 0; i < blank; i++)
                {
                    result += "0";
                }
                result += numberWantToset;

                WorkingHistory wh = entity as WorkingHistory;
                wh.WhId = result;
            }


            return entity;
        }
    }
}
