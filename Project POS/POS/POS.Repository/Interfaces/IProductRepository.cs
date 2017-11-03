using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Entities;

namespace POS.Repository.Interfaces
{
    public interface IProductRepository : IDisposable
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int productId);
        void InsertProduct(Product product);
        void DeleteProduct(int productId);
        void UpdateProduct(Product product);
        void Save();
    }
}