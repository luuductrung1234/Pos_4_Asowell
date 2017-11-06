using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Context
{
    public class AsowellConfiguration : DbConfiguration
    {
        public AsowellConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            DbInterception.Add(new AsowellInterceptorLogging());
        }
    }
}
