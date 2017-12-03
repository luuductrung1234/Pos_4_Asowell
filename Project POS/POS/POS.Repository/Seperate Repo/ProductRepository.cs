using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POS.Context;
using POS.Entities;

namespace POS.Repository.SeperateRepo
{
    public class ProductRepository : Interfaces.IProductRepository
    {
        private CloudContext context;

        public ProductRepository(CloudContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products.ToList();
        }

        public Product GetProductById(string productId)
        {
            return context.Products.Find(productId);
        }

        public void InsertProduct(Product product)
        {
            context.Products.Add(product);
        }

        public void DeleteProduct(string productId)
        {
            Product product = context.Products.Find(productId);
            context.Products.Remove(product);
        }

        public void UpdateProduct(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
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
