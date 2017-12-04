using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;

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
