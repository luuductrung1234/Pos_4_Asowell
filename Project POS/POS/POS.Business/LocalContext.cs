using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Entities;
using POS.Interfaces;
using POS.Mapping;

namespace POS.BusinessContext
{
    public partial class LocalContext : DbContext, ILocalContext
    {
        public System.Data.Entity.DbSet<ApplicationLog> ApplicationLogs { get; set; } // ApplicationLog
        public System.Data.Entity.DbSet<Table> Tables { get; set; } // Table
        public System.Data.Entity.DbSet<Chair> Chairs { get; set; } // Chair
        public System.Data.Entity.DbSet<OrderTemp> OrderTemps { get; set; } // OrderTemp
        public System.Data.Entity.DbSet<OrderDetailsTemp> OrderDetailsTemps { get; set; } // OrderDetailsTemp

        static LocalContext()
        {
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<LocalContext, MigrationConfiguration>());
        }

        public LocalContext()
            : base("Name=POSLocalConns")
        {
            InitializePartial();
        }

        public LocalContext(string connectionString)
            : base(connectionString)
        {
            InitializePartial();
        }

        public LocalContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model)
            : base(connectionString, model)
        {
            InitializePartial();
        }

        public LocalContext(System.Data.Common.DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            InitializePartial();
        }

        public LocalContext(System.Data.Common.DbConnection existingConnection, System.Data.Entity.Infrastructure.DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
            InitializePartial();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public bool IsSqlParameterNull(System.Data.SqlClient.SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as System.Data.SqlTypes.INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == System.DBNull.Value);
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Configurations.Add(new ApplicationLogMapping());
            modelBuilder.Configurations.Add(new ChairMapping());
            modelBuilder.Configurations.Add(new OrderDetailsTempMapping());
            modelBuilder.Configurations.Add(new OrderTempMapping());
            modelBuilder.Configurations.Add(new TableMapping());

            OnModelCreatingPartial(modelBuilder);
        }

        public static System.Data.Entity.DbModelBuilder CreateModel(System.Data.Entity.DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new ApplicationLogMapping(schema));
            modelBuilder.Configurations.Add(new ChairMapping(schema));
            modelBuilder.Configurations.Add(new OrderDetailsTempMapping(schema));
            modelBuilder.Configurations.Add(new OrderTempMapping(schema));
            modelBuilder.Configurations.Add(new TableMapping(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(System.Data.Entity.DbModelBuilder modelBuilder);
    }
}
