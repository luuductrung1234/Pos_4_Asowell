using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Entities;

namespace POS.Interfaces
{
    public interface ILocalContext
    {
        System.Data.Entity.DbSet<ApplicationLog> ApplicationLogs { get; set; } // ApplicationLog
        System.Data.Entity.DbSet<Table> Tables { get; set; } // Table
        System.Data.Entity.DbSet<Chair> Chairs { get; set; } // Chair
        System.Data.Entity.DbSet<OrderDetailsTemp> OrderDetailsTemps { get; set; } // OrderDetailsTemp
        System.Data.Entity.DbSet<OrderTemp> OrderTemps { get; set; } // OrderTemp

        int SaveChanges();
        System.Threading.Tasks.Task<int> SaveChangesAsync();
        System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken);
        System.Data.Entity.Infrastructure.DbChangeTracker ChangeTracker { get; }
        System.Data.Entity.Infrastructure.DbContextConfiguration Configuration { get; }
        System.Data.Entity.Database Database { get; }
        System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        System.Data.Entity.Infrastructure.DbEntityEntry Entry(object entity);
        System.Collections.Generic.IEnumerable<System.Data.Entity.Validation.DbEntityValidationResult> GetValidationErrors();
        System.Data.Entity.DbSet Set(System.Type entityType);
        System.Data.Entity.DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();
    }
}
