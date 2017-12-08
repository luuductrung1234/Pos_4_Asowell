using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;

namespace POS.Context
{
    public class AsowellConfiguration : DbConfiguration
    {
        public AsowellConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy(3, TimeSpan.FromMinutes(1)));
            DbInterception.Add(new AsowellInterceptorLogging());
        }
    }
}
