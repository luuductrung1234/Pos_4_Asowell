using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace POS.Context
{
    public static class ConnectWithExtension
    {
        /// <summary>
        /// Extension method that add the feature to change the default connection string of target DBContext class
        /// It help application change the connected database at runtime
        /// Notice:  all parameters are optional
        /// </summary>
        /// <param name="source"></param>
        /// <param name="initialCatalog">Database Name</param>
        /// <param name="dataSource">Server Address/Name</param>
        /// <param name="userId">UserId used to connect</param>
        /// <param name="password">Password used to connec</param>
        /// <param name="integratedSecuity">Security flag</param>
        /// <param name="configConnectionStringName">Name of this new connection string</param>
        /// <param name="applicationName">Name of the application do the connections</param>
        public static void ChangeDatabase(
            this CloudContext source,
            string initialCatalog = "",
            string dataSource = "",
            string userId = "",
            string password = "",
            bool integratedSecuity = true,
            string configConnectionStringName = "",
            string applicationName = "Asowell POS")
        /* this would be used if the
        *  connectionString name varied from 
        *  the base EF class name */
        {
            try
            {
                // use the const name if it's not null, otherwise
                // using the convention of connection string = EF contextname
                // grab the type name and we're done
                var configNameEf = string.IsNullOrEmpty(configConnectionStringName)
                    ? source.GetType().Name
                    : configConnectionStringName;

                // add a reference to System.Configuration
                var entityCnxStringBuilder = new EntityConnectionStringBuilder
                    (System.Configuration.ConfigurationManager
                        .ConnectionStrings[configNameEf].ConnectionString);

                // init the sqlbuilder with the full EF connectionstring cargo
                var sqlCnxStringBuilder = new SqlConnectionStringBuilder
                    (entityCnxStringBuilder.ProviderConnectionString);

                // only populate parameters with values if added
                if (!string.IsNullOrEmpty(initialCatalog))
                    sqlCnxStringBuilder.InitialCatalog = initialCatalog;
                if (!string.IsNullOrEmpty(dataSource))
                    sqlCnxStringBuilder.DataSource = dataSource;
                if (!string.IsNullOrEmpty(userId))
                    sqlCnxStringBuilder.UserID = userId;
                if (!string.IsNullOrEmpty(password))
                    sqlCnxStringBuilder.Password = password;

                // set the integrated security status
                sqlCnxStringBuilder.IntegratedSecurity = integratedSecuity;

                // set the application name that do the connection to database
                sqlCnxStringBuilder.ApplicationName = applicationName;

                // now flip the properties that were changed
                source.Database.Connection.ConnectionString
                    = sqlCnxStringBuilder.ConnectionString;
            }
            catch (Exception ex)
            {
                // manipulate exception
            }
        }
    }
}
